using System;
using System.Text.Json.Serialization;

namespace Tools.Swift.Connector.Model
{
    public class SwiftFile
    {
        [JsonPropertyName("bytes")]
        public ulong Bytes { get; set; }

        [JsonPropertyName("last_modified")]
        public DateTime LastModified { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
    }
}
