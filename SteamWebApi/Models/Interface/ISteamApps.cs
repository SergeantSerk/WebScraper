using System;
using System.Collections.Generic;
using System.Text;

namespace Steam.Models.Interface
{
    public interface ISteamApps
    {
        int appid { get; set; }
        string name { get; set; }
    }
}
