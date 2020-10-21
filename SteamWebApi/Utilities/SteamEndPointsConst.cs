using System;
using System.Collections.Generic;
using System.Text;

namespace Steam.Utilities
{
    public static class SteamEndPointsConst
    {

        public static  string AllGAMESENDPOINTS { get { return "https://api.steampowered.com/ISteamApps/GetAppList/v2/"; } }

        public static string APPDETAILSENDPOINT { get { return "https://store.steampowered.com/api/appdetails?appids="; } }
    }
}
