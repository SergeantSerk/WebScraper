
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

          var appList =   await api.GetAppList();
          var listOfAllApps = appList.apps;

            var app = listOfAllApps[2323];

            //"858740"

           var data =  await api.GetAppBySteamID(890280);
     


        }

    }
}
