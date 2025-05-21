using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Tools.Swift.Connector.Authentication;
using Tools.Swift.Connector.Exceptions;
using Tools.Swift.Connector.Model;

namespace Tools.Swift.Connector
{
    public class OVHSwiftClient : ISwiftClient
    {
        public readonly HttpClient _client;
        private readonly SwiftAuthentication _authorization;

        public HttpClient Client
        {
            get { return _client; }
        }

        public OVHSwiftClient(HttpClient client, SwiftAuthentication authentication)
        {
            ArgumentNullException.ThrowIfNull(client);

            _authorization = authentication;
            _client = client;
        }

        public async Task<IEnumerable<SwiftContainer>> GetAllContainersAsync()
        {
            List<SwiftContainer> data = [];
            string requesturi = $"{_authorization.Endpoint}?format=json";

            HttpRequestMessage request = new(HttpMethod.Get, requesturi);
            try
            {
                HttpResponseMessage response = await _client.SendAsync(request.AddToken(_authorization.Token));
                if (response.IsSuccessStatusCode)
                {
                    var respTxt = await response.Content.ReadAsStringAsync();
                    data = JsonSerializer.Deserialize<List<SwiftContainer>>(respTxt);
                }
            }
            catch (Exception ex)
            {
                throw new SwiftRequestException(ex.Message);
            }

            return data;
        }

        public async Task<IEnumerable<SwiftFile>> GetFilesByContainerAsync(string containername)
        {
            List<SwiftFile> data = [];
            string requesturi = $"{_authorization.Endpoint}/{HttpUtility.UrlEncode(containername)}?format=json";

            HttpRequestMessage request = new(HttpMethod.Get, requesturi);
            try
            {
                HttpResponseMessage response = await _client.SendAsync(request.AddToken(_authorization.Token));
                if (response.IsSuccessStatusCode)
                {
                    var respTxt = await response.Content.ReadAsStringAsync();
                    data = JsonSerializer.Deserialize<List<SwiftFile>>(respTxt);
                }
            }
            catch (Exception ex)
            {
                throw new SwiftRequestException(ex.Message);
            }

            return data;
        }

        public async Task<bool> CreateOrReplaceFileOnContainerAsync(string containername, string filename, byte[] data)
        {
            string requesturi = $"{_authorization.Endpoint}/{HttpUtility.UrlEncode(containername)}/{HttpUtility.UrlEncode(filename)}?format=json";

            HttpRequestMessage request = new(HttpMethod.Put, requesturi)
            {
                Content = new ByteArrayContent(data)
            };

            try
            {
                HttpResponseMessage response = await _client.SendAsync(request.AddToken(_authorization.Token));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new SwiftRequestException(ex.Message);
            }
        }

    }
}
