using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class CategoryModel
    {   [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}