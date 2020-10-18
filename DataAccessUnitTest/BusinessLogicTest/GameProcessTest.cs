using DataAccessLibrary.BusinessLogic;
using DataAccessLibrary.Interface;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities;
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
        public async Task CheckObjectProperties_ReturnTrueifAllPropertiesHaveValue()
        {
            var storeFound = "Steam";
            // retrieve store 

            var storeModel = new GameModel 
            { Publisher = "asdas", About = "About", Developer= "Develoepr",
                ReleaseDate = DateTime.Now, SteamDetailsID= 2, Thumbnail= "asds",
            Title = "helo world"};

          

            var t = DataValidatorHelper.HasAllEmptyProperties(storeModel);

            Assert.True(t);
          

        }










        //[Fact]
        //public async Task AddTag()
        //{
        //    var platformFound = "Steam";


        //    var p = new Tag { Title = platformFound };
        //    p.ID = await GameProcessor.AddTagAsync(p);


        //    // even if a store has been added even if it exist then update the info
        //    Assert.True(p.ID != 0);

        //}


        [Fact]
        public async Task AddGameTagShouldReturnNegativeWhenGameTagAlreadyExists()
        {

            var gameTag = new GameTagDetailsModel { GameID = 10, TagID = 2 };

            var addGameWithSteamModel = await GameProcessor.AddGameTagDetailsAsync(gameTag);

            Assert.True(addGameWithSteamModel != 0);

        }

        //[Fact]
        //public async Task AddSteamDetails()
        //{

        //    var steamDetails = new SteamDetailsModel
        //    { SteamID = "testID", SteamReview = "mostly positive", SteamReviewCount = 500 };


        //    var sd = await GameProcessor.AddSteamDetailsAsync(steamDetails);

        //    Assert.True(sd != 0);

        //}


        //[Fact]
        //public async Task AddDealShouldReturnIDOFAlreadyAddedDeal()
        //{
        //    var game = await GameProcessor.GetGameByTitleAsync(_testGameTitle2);

        //    var storeName = "Steam";


        //    var store = await  GameProcessor.GetStoreByNameAsync(storeName);



        //    var deal = new DealModel
        //    {
        //        GameID = game.ID,
        //        StoreID = store.ID,
        //        DatePosted = DateTime.Now,
        //        IsFree = false,
        //        Expired = false,
        //        ExpiringDate = DateTime.Now,
        //        URL = "http://www.testURL.com",
        //        LimitedTimeDeal = false,
        //        Price = 34.2m,
        //        PreviousPrice = 42.2m
        //    };



        //    var sd = await GameProcessor.AddDealAsync(deal);

        //    Assert.True(sd != 0);

        //}

        //[Fact]
        //public async Task AddGameTagDetailsShouldReturnCorrectRowUpdated()
        //{
        //    var steamTag = "Steam";

        //    var game = await GameProcessor.GetGameByTitleAsync(_testGameTitle2);

        //    var tag = await GameProcessor.GetTagByTitleAsync(steamTag);

        //    var gameTagDetails = new GameTagDetailsModel { GameID = game.ID, TagID=tag.ID};


        //    var sd = await GameProcessor.AddGameTagDetailsAsync(gameTagDetails);


        //    var g = new List<GameTagDetailsModel>();

        //    g.Add(gameTagDetails);

        //    var gtd2= new GameTagDetailsModel { GameID = game.ID, TagID = 1 };

        //    g.Add(gtd2);

        //    var agt = await GameProcessor.AddGameTagDetailsAsync(g);

        //    var expectedValue = 0;

        //    Assert.True(expectedValue == agt);

        //}


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
