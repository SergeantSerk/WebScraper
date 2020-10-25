
using DataAccessLibrary.BusinessLogic;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using Steam.Utilities;
using Steam.WebApi;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
           

            var api = Factory.GetSteamAPI();

          var apps =   await api.GetApps();
       

        

            //"858740"


      var data = await api.GetAppBySteamID(1080110);
       
     


        }

    }
}
