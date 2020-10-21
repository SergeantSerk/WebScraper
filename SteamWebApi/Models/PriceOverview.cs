using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Steam.Models
{
   public class PriceOverview
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("initial")]
        public int Initial { get; set; }
        [JsonPropertyName("final")]
        public int Final { get; set; }
        [JsonPropertyName("discount_percent")]
        public decimal DiscountPercentage { get; set; }
        [JsonPropertyName("initial_formatted")]
        public string InitialFormat { get; set; }
        [JsonPropertyName("final_formatted")]
        public string FinalFormat { get; set; }
    }
}
