using Steam.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Steam.Models
{
    public class SteamApp : ISteamApp
    {
        public int appid { get; set ; }
        public string name { get ; set ; }


    }
}
