using Steam.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Package
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("selection_text")]
        public string SelectionText { get; set; }
        [JsonPropertyName("save_text")]
        public string SaveText { get; set; }

        [JsonPropertyName("display_type")]
        [JsonConverter(typeof(StringToIntJSONConverter))]
        public int DisplayType { get; set; }
        [JsonPropertyName("is_recurring_subscription")]
        public string IsRecurringSubscription { get; set; }
        [JsonPropertyName("subs")]
        public List<SubModels> Subs { get; set; }
    }
}
