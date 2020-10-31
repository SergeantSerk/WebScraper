using Steam.Interfaces;
using Steam.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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
