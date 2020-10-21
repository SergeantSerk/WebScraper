using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class GenreModel
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}