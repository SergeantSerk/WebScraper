using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class Sub
    {
        [JsonPropertyName("packageid")]
        public int PackageId { get; set; }
        [JsonPropertyName("percent_savings_text")]
        public string PercentSavingText { get; set; }
        [JsonPropertyName("percent_savings")]
        public int PercentSavings { get; set; }
        [JsonPropertyName("option_text")]
        public string OptionText { get; set; }
        [JsonPropertyName("option_description")]
        public string OptionDescription { get; set; }
        [JsonPropertyName("can_get_free_license")]
        public string CanGetFreeLicense { get; set; }
        [JsonPropertyName("is_free_license")]
        public bool IsFreeLicense { get; set; }
        [JsonPropertyName("price_in_cents_with_discount")]
        public int PriceInCentWithDiscount { get; set; }

    }
}