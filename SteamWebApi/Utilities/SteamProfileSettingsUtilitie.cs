using Steam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Steam.Utilities
{
    public static class SteamProfileSettingsUtilitie
    {
        private const string _settingFilePath =
           @"C:\Users\Abdul\source\repos\Webscraper\SteamWebApi\SteamProfilesetting.json";
        public async static Task<SteamProfileSettings> GetSteamProfileSettingsAsync()
        {
            var data = await File.ReadAllTextAsync(_settingFilePath);

            var jsonParsed = JsonSerializer.Deserialize<SteamProfileSettings>(data);

            return jsonParsed;
        }

        public async static void UpdateSteamProfileSettingAsync(SteamProfileSettings profileSettings)
        {
            var parsedData = JsonSerializer.Serialize<SteamProfileSettings>(profileSettings);
            File.WriteAllTextAsync(_settingFilePath, parsedData);
        }
    }
}
