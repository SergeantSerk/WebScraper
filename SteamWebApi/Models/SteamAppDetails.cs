using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Steam.Models
{
    public class SteamAppDetails
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("steam_appid")]
        public int SteamAppID { get; set; }
        [JsonPropertyName("required_age")]
        public int RequiredAge { get; set; }
        [JsonPropertyName("is_free")]
        public bool IsFree { get; set; }
        [JsonPropertyName("detailed_description")]
        public string Description { get; set; }
        [JsonPropertyName("reviews")]
        public string Reviews { get; set; }
        [JsonPropertyName("header_image")]
        public string HeaderImage { get; set; }
        [JsonPropertyName("website")]
        public string Website { get; set; }
        [JsonPropertyName("pc_requirements")]
        public Dictionary<string, object> PcRequirement { get; set; }
        [JsonPropertyName("mac_requirements")]
        public Dictionary<string, object> MacRequirement { get; set; }
        [JsonPropertyName("linux_requirements")]
        public Dictionary<string, object> LinuxRequirement { get; set; }
        [JsonPropertyName("developers")]
        public List<string> Developers { get; set; }
        [JsonPropertyName("publishers")]
        public List<string> Publishers { get; set; }
        [JsonPropertyName("price_overview")]
        public PriceOverview PriceOverview { get; set; }
        [JsonPropertyName("packages")]
        public List<int> Packages { get; set; }
        [JsonPropertyName("package_groups")]
        public List<Package> PackageGroups { get; set; }
        [JsonPropertyName("platforms")]
        public Platform Platforms { get; set; }
        [JsonPropertyName("categories")]
        public List<Category> Categories { get; set; }
        [JsonPropertyName("genres")]
        public List<GenreModel> Genres { get; set; }
        [JsonPropertyName("screenshots")]
        public List<Screenshot> Screenshots { get; set; }
        [JsonPropertyName("movies")]
        public List<Movie> Movies { get; set; }
        [JsonPropertyName("achievements")]
        public Achievement Achievements { get; set; }
        [JsonPropertyName("release_date")]
        public DateModel ReleaseDate { get; set; }

        [JsonPropertyName("support_info")]
        public SupportInfo SupportInfo { get; set; }
        [JsonPropertyName("background")]
        public string Background { get; set; }
        [JsonPropertyName("content_descriptors")]
        public ContentDescriptor ContentDescriptor { get; set; }



    }
}
