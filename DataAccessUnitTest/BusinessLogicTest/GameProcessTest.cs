using DataAccessLibrary.BusinessLogic;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessLibraryTest.BusinessLogicTest
{
    public class GameProcessTest
    {

        [Fact]
        public async Task AddGame()
        {

            var game = new GameModel()
            {
                ReleaseDate = DateTime.Now,
                About = "About",
                Developer = "Developer",
                Thumbnail = "Thumbnail",
                Publisher = "Publisher",
                Title = "Title2"

            };

            var steamDetails = new SteamDetailsModel { SteamID = "TestSteamID2", SteamReview = "Mostly Positive", SteamReviewCount = 0 };

            var checkGame = GameProcessor.GetFullGameByTitle(game.Title);

            var tasks = new List<Task>();

            if(checkGame == null)
            {
                
                var steamDetailsID = await GameProcessor.AddSteamDetailsAsync(steamDetails);

                game.SteamDetailsID = steamDetailsID;

               var gameID = await GameProcessor.AddGameAsync(game);

                var platformFound = "Steam";

                var platform = await GameProcessor.GetPlatformByTitleAsync(platformFound);

                if(platform == null)
                {
                    var p = new Platform { Title = platformFound };
                    p.ID = await GameProcessor.AddPlatformAsync(p);
                    platform = p;
                }

                var sr = new SystemRequirement
                {
                    GameID = gameID,
                    PlatformID = platform.ID,
                    Memory = "32GB",
                    Os = "Windows 7",
                    Processor = "i5",
                    Requirement = "TestRequirement",
                    Storage = "4gb", 
                    
                };

                await GameProcessor.AddSystemRequirementAsync(sr);

                var storeFound = "Steam";

                var store = await GameProcessor.GetStoreByNameAsync(storeFound);

                if(store == null)
                {
                    var storeModel = new StoreModel { Name = storeFound, Logo = "TestLogo" };

                    storeModel.ID = await GameProcessor.AddStoreAsync(storeModel);

                    store = storeModel;
                }
                var deal = new DealModel
                {
                    GameID = gameID,
                    StoreID = store.ID,
                    Price = 32.2m,
                    PreviousPrice = 40.23m,
                    Expired = false,
                    ExpiringDate = DateTime.Now,
                    DatePosted = DateTime.Now,
                    LimitedTimeDeal = false,
                    URL = "TestDealUrl",
                    IsFree = false
                };

                await GameProcessor.AddDealAsync(deal);


            }

          
            //Assert.Null(checkGame);


            //// add games and return last row
            //var gameAdded = GameProcessor.AddGame(game);


        }


       
        [Fact]
        public async Task AddStore()
        {
            var storeFound = "Steam";
            // retrieve store 
       
            var storeModel = new StoreModel { Name = storeFound, Logo = "TestLogo" };


            var store = await GameProcessor.AddStoreAsync(storeModel);

            // even if a store has been added even if it exist then update the info
            Assert.True(store != 0);

        }

        [Fact]
        public async Task AddPlatform()
        {
            var platformFound = "Steam";

            
           var p = new Platform { Title = platformFound };
            p.ID = await GameProcessor.AddPlatformAsync(p);
                
        
            // even if a store has been added even if it exist then update the info
            Assert.True(p.ID != 0);

        }

        [Fact]
        public async Task AddTag()
        {
            var platformFound = "Steam";


            var p = new Tag { Title = platformFound };
            p.ID = await GameProcessor.AddTagAsync(p);


            // even if a store has been added even if it exist then update the info
            Assert.True(p.ID != 0);

        }




        [Fact]
        public void GetAllFullGame()
        {

            var allGames = GameProcessor.GetAllFullGames();
            Assert.True(allGames.Count > 0);
        }


        [Fact]
        public void GetFullGameById()
        {

            var allGames = GameProcessor.GetFullGameById(1);


            Assert.NotNull(allGames);

        }


        [Fact]
        public void GetFullGameByTitle()
        {

            var allGames = GameProcessor.GetFullGameByTitle("TestTitle1");


            Assert.NotNull(allGames);

        }
    }
}
