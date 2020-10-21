using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class SystemRequirement
    {
        [JsonPropertyName("minimum")]
        public string Minimum { get; set; }
    }
}