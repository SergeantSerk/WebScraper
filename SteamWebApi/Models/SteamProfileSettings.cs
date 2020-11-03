using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class SteamProfileSettings
    {
        [JsonPropertyName("currentIndex")]
        public int CurrentIndex { get; set; }
        [JsonPropertyName("previousIndex")]
        public int PreviousIndex { get; set; }
    }
}
