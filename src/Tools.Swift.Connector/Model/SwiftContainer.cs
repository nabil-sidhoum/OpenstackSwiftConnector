using System;
using System.Text.Json.Serialization;

namespace Tools.Swift.Connector.Model
{
    public class SwiftContainer
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("last_modified")]
        public DateTime LastModified { get; set; }

        [JsonPropertyName("bytes")]
        public long Bytes { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
