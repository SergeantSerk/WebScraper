using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Movie
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonPropertyName("webm")]
        public Video Webm { get; set; }
        [JsonPropertyName("mp4")]
        public Video MP4 { get; set; }
        [JsonPropertyName("highlight")]
        public bool Highlight { get; set; }
    }
}