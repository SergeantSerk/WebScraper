using Steam.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Steam.Utilities
{
    public static class Factory
    {

        public static HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        public static SteamAPI GetSteamAPI()
        {

            return new SteamAPI(GetHttpClient());
        }
    }
}
