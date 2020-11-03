using Steam.Interfaces;
using Steam.Models;
using Steam.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Steam.Utilities
{
    public static class SteamFactory
    {
       

        public static HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        public static ISteamAPI GetSteamAPI()
        {

            return new SteamAPI(GetHttpClient());
        }

     
    }
}
