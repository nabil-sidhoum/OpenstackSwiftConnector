using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tools.Swift.Connector.Authentication
{
    public class SwiftAuthenticationRequest
    {
        [JsonPropertyName("auth")]
        public Auth Authentication { get; set; }


        public class Auth
        {
            [JsonPropertyName("identity")]
            public Identity Identity { get; set; }
        }

        public class Domain
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
        }

        public class Identity
        {
            [JsonPropertyName("methods")]
            public List<string> Methods { get; set; }

            [JsonPropertyName("password")]
            public Password Password { get; set; }
        }

        public class Password
        {
            [JsonPropertyName("user")]
            public User User { get; set; }
        }

        public class User
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("domain")]
            public Domain Domain { get; set; }

            [JsonPropertyName("password")]
            public string Password { get; set; }
        }
    }
}
