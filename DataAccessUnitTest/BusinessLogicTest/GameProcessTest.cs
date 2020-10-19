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


        [Fact]
        public async Task AddDeal_ShouldAddDealsIfExistingisNotValidORExpired()
        {

            var deal = new DealModel
            {
                GameID = 10,
                StoreID = 4,
                Expired = false,
                ExpiringDate = DateTime.Now,
                DatePosted = DateTime.Now,
                IsFree = true,
                Price = 32.4m,
                PreviousPrice = 43.2m,
                LimitedTimeDeal = true,
                URL = @"https://www.youtube.com/watch?v=O901ZEBQ230&t=4s&ab_channel=ApnaJ"
            };


            var adDB = await GameProcessor.AddDealAsync(deal);

            // even if a store has been added even if it exist then update the info
            Assert.True(adDB != 0);

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
        public async Task AddGameTagShouldReturnNegativeWhenGameTagAlreadyExists()
        {

            var gameTag = new GameTagDetailsModel { GameID = 10, TagID = 2 };

            var addGameWithSteamModel = await GameProcessor.AddGameTagDetailsAsync(gameTag);

            Assert.True(addGameWithSteamModel != 0);

        }


        [Fact]
        public async Task AddSteamDetails_ShouldReturnIDOfExistingItem()
        {

            var sd = new SteamDetailsModel { SteamID = "A2", SteamReview= "Mostly Positive" };

            var addSteamDetailsModel = await GameProcessor.AddSteamDetailsAsync(sd);

            Assert.True(addSteamDetailsModel != 0);

        }

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

        [Fact]
        public async Task AddSystemRequirement_ShouldReturn0IfGameAndPlatformIDDontExistONDB()
        {

            var sr = new SystemRequirement
            {
                GameID = 1,
                PlatformID = 1,
                Memory = "5gb",
                MinimumSystemRequirement = true,
                Storage = "43gb",
                Os = "windows",
                Processor = "I7",
                
            };

            var t = await GameProcessor.AddSystemRequirementAsync(sr);

            Assert.True(t != 0);

            var sr2 = new SystemRequirement
            {
                GameID = 3,
                PlatformID = 4,
                Memory = "5gb",
                MinimumSystemRequirement = true,
                Storage = "43gb",
                Os = "windows",
                Processor = "I7",

            };

            var sr2AddDB = await GameProcessor.AddSystemRequirementAsync(sr2);

            Assert.Equal(0, sr2AddDB);

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
