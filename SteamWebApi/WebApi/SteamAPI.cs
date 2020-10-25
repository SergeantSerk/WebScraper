using Steam.Models;
using Steam.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace Steam.WebApi
{
    public class SteamAPI
    {

        private HttpClient _httpclient;

        public SteamAPI(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }
        
       
        public async Task<List<SteamApp>> GetApps()
        {
            var data = await _httpclient.GetStringAsync(SteamEndPointsConst.AllGAMESENDPOINTS);


            return JsonSerializer.Deserialize<SteamAppsJsonModel>(data).applist.apps;

        }

        public async Task<SteamAppDetails> GetAppBySteamID(int steamID)
        {
            using (var data = await _httpclient.GetStreamAsync(SteamEndPointsConst.APPDETAILSENDPOINT + steamID))
            {

                var deserializedData = await JsonSerializer.DeserializeAsync<Dictionary<string, SteamAppsDetailsJsonModel>>(data);

                var parsedData = deserializedData[steamID.ToString()];

                return parsedData.Success ? parsedData.Data : null;
            }
        }
    }



}
