using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Platform
    {
        [JsonPropertyName("windows")]
        public bool Window { get; set; }

        [JsonPropertyName("Mac")]
        public bool Mac { get; set; }

        [JsonPropertyName("Linux")]
        public bool Linux { get; set; }
    }
}
