
using BusinessAccessLibrary.Factories;
using BusinessAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabasePostModels;
using Steam.Processors;
using Steam.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
           

            var api = SteamFactory.GetSteamAPI();
            var gameManager = BALFactory.GetGameManager();

            var steamProcessor = new SteamProcessors(api, gameManager);

            await steamProcessor.Start();






            //"858740"
            //1080110

            //foreach (var app in apps)
            //{

            //    var fullApp = await api.GetAppBySteamID(app.appid);

            //    if (fullApp != null)
            //    {

            //        var gamemodel = new GameAddModel()
            //        {
            //            Title = fullApp.Name,
            //            Type = fullApp.Type,
            //            Website = fullApp.Website,
            //            Description = fullApp.Description,
            //            HeaderImage = fullApp.HeaderImage,
            //            Background = fullApp.Background



            //        };
            //        gameManager.AddGameAsync(gamemodel);

            //       await Task.Delay(1500);
            //    }

            //}

            //var fullApp = await api.GetAppBySteamID(1080110);




        }

    }
}
