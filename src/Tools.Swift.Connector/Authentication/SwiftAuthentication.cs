using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tools.Swift.Connector.Configuration;
using Tools.Swift.Connector.Exceptions;

namespace Tools.Swift.Connector.Authentication
{
    public class SwiftAuthentication
    {
        private static readonly object _tokenlock = new();

        private readonly HttpClient _client;
        private readonly SwiftAuthenticationConfig _config;
        private static SwiftAuthenticationToken _token;

        public string Token
        {
            get
            {
                lock (_tokenlock)
                {
                    if (_token is null || _token.ExpiresOn < DateTime.UtcNow.AddMinutes(15))
                        AuthenticateAsync().GetAwaiter().GetResult();
                }
                return _token.AccessToken;
            }
        }

        public string Endpoint { get; private set; }

        public SwiftAuthentication(HttpClient client, SwiftAuthenticationConfig config)
        {
            ArgumentNullException.ThrowIfNull(client);

            ArgumentNullException.ThrowIfNull(config);

            _client = client;
            _config = config;

            AuthenticateAsync().GetAwaiter().GetResult();
        }

        private async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            string tokenUrl = $"{_config.Authurl}/auth/tokens";

            SwiftAuthenticationRequest request = new()
            {
                Authentication = new()
                {
                    Identity = new()
                    {
                        Methods = ["password"],
                        Password = new()
                        {
                            User = new()
                            {
                                Name = _config.Username,
                                Password = _config.Password,
                                Domain = new()
                                {
                                    Id = "default"
                                }
                            }
                        }
                    }
                }
            };
            string contentStr = JsonSerializer.Serialize(request);

            StringContent content = new(contentStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            try
            {
                var resp = await _client.PostAsync(tokenUrl, content);
                if (resp.IsSuccessStatusCode)
                {
                    var respTxt = await resp.Content.ReadAsStringAsync();
                    SwiftAuthenticationResponse result = JsonSerializer.Deserialize<SwiftAuthenticationResponse>(respTxt);

                    //Initialisation du Token
                    _token = new()
                    {
                        AccessToken = resp.Headers.ToDictionary(a => a.Key, a => string.Join(";", a.Value))["x-subject-token"],
                        ExpiresOn = result.AccessToken.ExpiresAt
                    };

                    //Initialisation du Endpoint
                    SwiftAuthenticationResponse.Catalog swiftcatalog = result.AccessToken.Catalog.First(c => c.Name == _config.Catalog);
                    SwiftAuthenticationResponse.Endpoint endpoint = swiftcatalog.Endpoints.First(ep => ep.Region == _config.Region);
                    Endpoint = endpoint.Url;
                }
                else
                {
                    throw new SwiftAuthenticationException(await resp.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                throw new SwiftAuthenticationException(ex.Message);
            }

            return success;
        }
    }
}
