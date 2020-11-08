using Steam.Models;
using Steam.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using Steam.Interfaces;

namespace Steam.WebApi
{
    public class SteamAPI : ISteamAPI
    {

        private HttpClient _httpclient;

        private int _retryCount = 0;
        private int _currentClientIndex;
        //private List<HttpClient> _clients = SteamFactory.GetHttpClients();

        private bool _internetConnection = true;
        private TimeSpan _coolDownTime = TimeSpan.FromMinutes(5);

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
            while (_retryCount != 5)
            {

                try
                {
                    Console.WriteLine(steamID);

                    using (var data = await _httpclient.GetStreamAsync(SteamEndPointsConst.APPDETAILSENDPOINT + steamID))
                    {
                        _internetConnection = true;
                        _retryCount = 0;
                        try
                        {
                            var deserializedData = await JsonSerializer.DeserializeAsync<Dictionary<string, SteamAppsDetailsJsonModel>>(data);
                            var parsedData = deserializedData[steamID.ToString()];

                            return parsedData.Success ? parsedData.Data : null;
                        }
                        catch (JsonException e)
                        {
                            Console.WriteLine($" {e} No Json is avaliable");

                            return null;
                        }
                    }
                }//No such host is known
                catch (HttpRequestException e)
                {
                    // pause for  5minute
                    _retryCount++;

                    Console.WriteLine($"Internet disconnected... Waiting 5 minute before reconnecting");

                    await Task.Delay(_coolDownTime);

                    Console.WriteLine($"No Internet connection, Attempting to reconnect...");

                    Console.WriteLine($"Number of Attempts : {_retryCount}.");
                }
                catch(TaskCanceledException e)
                {
                    _retryCount++;

                    Console.WriteLine($"Internet disconnected... Waiting 5 minute before reconnecting");

                    await Task.Delay(_coolDownTime);

                    Console.WriteLine($"No Internet connection, Attempting to reconnect...");
                    Console.WriteLine($"Number of Attempts : {_retryCount}.");

                }
            }

            throw new HttpRequestException("5 Attempts were made to reconnect to the internet");
        }

        // need to work on this

        //public async Task<SteamAppDetails> GetAppBySteamIDProxies(int steamID)
        //{

        //    var clientIndex = GetCurrentClientIndex();

        //    var client = _clients[clientIndex];


        //    using (var data = await client.GetStreamAsync(SteamEndPointsConst.APPDETAILSENDPOINT + steamID))
        //    {
        //        try
        //        {
        //            var deserializedData = await JsonSerializer.DeserializeAsync<Dictionary<string, SteamAppsDetailsJsonModel>>(data);
        //            var parsedData = deserializedData[steamID.ToString()];

        //            return parsedData.Success ? parsedData.Data : null;
        //        }
        //        catch (JsonException e)
        //        {
        //            Console.WriteLine($" {e} No Json is avaliable");

        //            return null;
        //        }
        //    }
        //}



        //private int GetCurrentClientIndex()
        //{

        //    if (_currentClientIndex == _clients.Count)
        //    {
        //        _currentClientIndex = 0;
        //        return _currentClientIndex;

        //    }
        //    else
        //    {
        //       return  _currentClientIndex++;
        //    }

        //}


    }



}
