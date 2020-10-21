using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class DateModel
    {
        [JsonPropertyName("coming_soon")]
        public bool ComingSoon { get; set; }
        [JsonPropertyName("date")]
        public string ReleaseDate { get; set; }
    }
}