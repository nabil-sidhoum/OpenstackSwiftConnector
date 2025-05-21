using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tools.Swift.Connector.Authentication
{
    public class SwiftAuthenticationResponse
    {
        [JsonPropertyName("token")]
        public Token AccessToken { get; set; }

        public class Catalog
        {
            [JsonPropertyName("endpoints")]
            public List<Endpoint> Endpoints { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Domain
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Endpoint
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("interface")]
            public string Interface { get; set; }

            [JsonPropertyName("region_id")]
            public string RegionId { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("region")]
            public string Region { get; set; }
        }

        public class Project
        {
            [JsonPropertyName("domain")]
            public Domain Domain { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Role
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Token
        {
            [JsonPropertyName("methods")]
            public List<string> Methods { get; set; }

            [JsonPropertyName("user")]
            public User User { get; set; }

            [JsonPropertyName("audit_ids")]
            public List<string> AuditIds { get; set; }

            [JsonPropertyName("expires_at")]
            public DateTime ExpiresAt { get; set; }

            [JsonPropertyName("issued_at")]
            public DateTime IssuedAt { get; set; }

            [JsonPropertyName("project")]
            public Project Project { get; set; }

            [JsonPropertyName("is_domain")]
            public bool IsDomain { get; set; }

            [JsonPropertyName("roles")]
            public List<Role> Roles { get; set; }

            [JsonPropertyName("is_admin_project")]
            public bool IsAdminProject { get; set; }

            [JsonPropertyName("catalog")]
            public List<Catalog> Catalog { get; set; }
        }

        public class User
        {
            [JsonPropertyName("domain")]
            public Domain Domain { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("password_expires_at")]
            public object PasswordExpiresAt { get; set; }
        }
    }
}
