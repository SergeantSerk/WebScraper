using Steam.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class ContentDescriptor
    {
        [JsonPropertyName("ids")]
        public List<int> IDS { get; set; }
        [JsonPropertyName("notes")]
        [JsonConverter(typeof(StringFromArrayJSONConvertor))]
        public string Notes { get; set; }
    }
}