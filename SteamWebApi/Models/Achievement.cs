using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Achievement
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
        public List<Highlight> Highlights { get; set; }
    }
}