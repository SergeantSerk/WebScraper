using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class SupportInfo
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}