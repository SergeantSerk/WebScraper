using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class ContentDescriptor
    {
        [JsonPropertyName("ids")]
        public List<int> IDS { get; set; }
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; }
    }
}