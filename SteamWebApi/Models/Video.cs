using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Video
    {
        [JsonPropertyName("480")]
        public string Quality { get; set; }
        [JsonPropertyName("max")]
        public string Max { get; set; }
    }
}