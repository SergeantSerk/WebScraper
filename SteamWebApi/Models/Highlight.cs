using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Highlight
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}