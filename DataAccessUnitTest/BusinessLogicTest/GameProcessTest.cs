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

        private string _testGameTitle = "TestGame";
        private string _testGameTitle2 = "TestGame2";


        //[Fact]
        //public async Task AddGame()
        //{

        //    var game = new GameModel()
        //    {
        //        ReleaseDate = DateTime.Now,
        //        About = "About",
        //        Developer = "Developer",
        //        Thumbnail = "Thumbnail",
        //        Publisher = "Publisher",
        //        Title = "Title2"

        //    };

        //    var steamDetails = new SteamDetailsModel { SteamID = "TestSteamID2", SteamReview = "Mostly Positive", SteamReviewCount = 0 };

        //    var checkGame = GameProcessor.GetFullGameByTitle(game.Title);

        //    var tasks = new List<Task>();

        //    if(checkGame == null)
        //    {

        //        var steamDetailsID = await GameProcessor.AddSteamDetailsAsync(steamDetails);

        //        game.SteamDetailsID = steamDetailsID;

        //       var gameID = await GameProcessor.AddGameAsync(game);

        //        var platformFound = "Steam";

        //        var platform = await GameProcessor.GetPlatformByTitleAsync(platformFound);

        //        if(platform == null)
        //        {
        //            var p = new Platform { Title = platformFound };
        //            p.ID = await GameProcessor.AddPlatformAsync(p);
        //            platform = p;
        //        }

        //        var sr = new SystemRequirement
        //        {
        //            GameID = gameID,
        //            PlatformID = platform.ID,
        //            Memory = "32GB",
        //            Os = "Windows 7",
        //            Processor = "i5",
        //            Requirement = "TestRequirement",
        //            Storage = "4gb", 

        //        };

        //        await GameProcessor.AddSystemRequirementAsync(sr);

        //        var storeFound = "Steam";

        //        var store = await GameProcessor.GetStoreByNameAsync(storeFound);

        //        if(store == null)
        //        {
        //            var storeModel = new StoreModel { Name = storeFound, Logo = "TestLogo" };

        //            storeModel.ID = await GameProcessor.AddStoreAsync(storeModel);

        //            store = storeModel;
        //        }
        //        var deal = new DealModel
        //        {
        //            GameID = gameID,
        //            StoreID = store.ID,
        //            Price = 32.2m,
        //            PreviousPrice = 40.23m,
        //            Expired = false,
        //            ExpiringDate = DateTime.Now,
        //            DatePosted = DateTime.Now,
        //            LimitedTimeDeal = false,
        //            URL = "TestDealUrl",
        //            IsFree = false
        //        };

        //        await GameProcessor.AddDealAsync(deal);


        //    }
        //

        //Assert.Null(checkGame);


        //// add games and return last row
        //var gameAdded = GameProcessor.AddGame(game);


        // }



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
        public async Task AddGameShouldReturnNegativeWhenGameAlreadyExists()
        {
            var game = new GameModel()
            {
                ReleaseDate = DateTime.Now,
                About = "About",
                Developer = "Developer",
                Thumbnail = "Thumbnail",
                Publisher = "Publisher",
                Title = "Title4"

            };


            var g = await GameProcessor.AddGameAsync(game);

           
            Assert.True(g != 0);

            var gFromDatabase = await GameProcessor.GetGameByIdAsync(g);

            Assert.True(gFromDatabase.SteamDetailsID == 0);

            var game2 = new GameModel()
            {
                ReleaseDate = DateTime.Now,
                About = "About",
                Developer = "Developer",
                Thumbnail = "Thumbnail",
                Publisher = "Publisher",
                Title = _testGameTitle2

            };

            var steamDetailsTest = new SteamDetailsModel
            {
                SteamID = "TestDummySteamID",
                SteamReview = "Mostly Positive",
                SteamReviewCount = 500
            };

            game2.SteamDetailsID = await GameProcessor.AddSteamDetailsAsync(steamDetailsTest);

            var addGameWithSteamModel = await GameProcessor.AddSteamGameAsync(game2);

            var getGameFromDb = await GameProcessor.GetGameByIdAsync(addGameWithSteamModel);

            Assert.True(getGameFromDb.SteamDetailsID != 0);

        }

        [Fact]
        public async Task AddSteamDetails()
        {

            var steamDetails = new SteamDetailsModel
            { SteamID = "testID", SteamReview = "mostly positive", SteamReviewCount = 500 };


            var sd = await GameProcessor.AddSteamDetailsAsync(steamDetails);

            Assert.True(sd != 0);

        }


        [Fact]
        public async Task AddDealShouldReturnIDOFAlreadyAddedDeal()
        {
            var game = await GameProcessor.GetGameByTitleAsync(_testGameTitle2);

            var storeName = "Steam";


            var store = await  GameProcessor.GetStoreByNameAsync(storeName);



            var deal = new DealModel
            {
                GameID = game.ID,
                StoreID = store.ID,
                DatePosted = DateTime.Now,
                IsFree = false,
                Expired = false,
                ExpiringDate = DateTime.Now,
                URL = "http://www.testURL.com",
                LimitedTimeDeal = false,
                Price = 34.2m,
                PreviousPrice = 42.2m
            };

           

            var sd = await GameProcessor.AddDealAsync(deal);

            Assert.True(sd != 0);

        }

        [Fact]
        public async Task AddGameTagDetailsShouldReturnCorrectRowUpdated()
        {
            var steamTag = "Steam";

            var game = await GameProcessor.GetGameByTitleAsync(_testGameTitle2);

            var tag = await GameProcessor.GetTagByTitleAsync(steamTag);

            var gameTagDetails = new GameTagDetailsModel { GameID = game.ID, TagID=tag.ID};


            var sd = await GameProcessor.AddGameTagDetailsAsync(gameTagDetails);


            var g = new List<GameTagDetailsModel>();

            g.Add(gameTagDetails);

            var gtd2= new GameTagDetailsModel { GameID = game.ID, TagID = 1 };

            g.Add(gtd2);

            var agt = await GameProcessor.AddGameTagDetailsAsync(g);

            var expectedValue = 0;

            Assert.True(expectedValue == agt);

        }


        [Fact]
        public async Task GetAllFullGame_ShouldReturnDealsThatAreNotExpired()
        {

            var expiredDealExist = false;

            var allGames = await GameProcessor.GetAllFullGames();

            foreach(var game in allGames)
            {
                foreach(var deal in game.Deals)
                {
                    if (deal.Expired)
                    {
                        expiredDealExist = true;
                        break;
                    }
                }
            }
            
            Assert.False(expiredDealExist);
        }




        [Fact]
        public async Task GetFullGameById_ShouldReturnTestData()
        {

            var allGames = await GameProcessor.GetFullGameById(1);


            Assert.NotNull(allGames);

        }


        [Fact]
        public async Task GetFullGameByTitleShouldReturnNullforInvalidGame()
        {

            var allGames = await GameProcessor.GetFullGameByTitle("TestTitle1");


            Assert.Null(allGames);

        }
    }
}
