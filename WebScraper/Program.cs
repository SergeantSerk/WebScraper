
using BusinessAccessLibrary.Factories;
using BusinessAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabasePostModels;
using Steam.Utilities;

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

        IGameManager gameManager = BALFactory.GetGameManager();
        var gamemodel = new GameAddModel();
            gameManager.AddGameAsync(gamemodel);
     


        }

    }
}
