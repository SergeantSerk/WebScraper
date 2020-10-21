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
    public class GameBusinessAdditionTest
    {

      
        private string _testGameTitle2 = "TestGame2";
        private const string _testSteamDetails = "A2";
        private const string _testPlatform = "SteamTest";
        private const string _teststore = "TestStore";
        private const string _testTag = "TestTag";


        [Fact]
        public async Task AddSteamDetails_ShouldReturnIDOfExistingItem()
        {

            var sd = new SteamDetailsModel { SteamID = _testSteamDetails, SteamReview = "Mostly Positive" };

            var addSteamDetailsModel = await GameBusinessAddition.AddSteamDetailsAsync(sd);

            Assert.True(addSteamDetailsModel != 0);

        }



        [Fact]
        public async Task AddGameToDB_ShouldReturnIDOfExistingItem()
        {
            var steamdetals = await GameBusinessAccess.GetSteamDetailsBySteamIdAsync(_testSteamDetails);

            var game = new GameModel
            {
                About = "TestAbout",
                Developer = "TestDeveloper",
                Publisher = "TestPublisher",
                ReleaseDate = DateTime.Now,
                Thumbnail = "TestThumbnailUrl",
                Title = _testGameTitle2,
                SteamDetailsID = steamdetals.ID
            };

            var addGameToDb = await GameBusinessAddition.AddGameAsync(game);

            Assert.True(addGameToDb != 0);

        }

        [Fact]
        public async Task AddStore_ShouldReturnTheIDWhenGameTagAlreadyExists()
        {
           
       
            var storeModel = new StoreModel { Name = _teststore, Logo = "TestLogo" };


            var store = await GameBusinessAddition.AddStoreAsync(storeModel);

            // even if a store has been added even if it exist then update the info
            Assert.True(store != 0);

        }

        [Fact]
        public async Task AddPlatform()
        {
           
            
           var p = new Platform { Title = _testPlatform };
            p.ID = await GameBusinessAddition.AddPlatformAsync(p);
                
        
            // even if a store has been added even if it exist then update the info
            Assert.True(p.ID != 0);

        }



        [Fact]
        public async Task AddDeal_ShouldAddDealsIfExistingisNotValidORExpired()
        {

            var game =  await GameBusinessAccess.GetGameByTitleAsync(_testGameTitle2);

            var store = await GameBusinessAccess.GetStoreByNameAsync(_teststore);

            var deal = new DealModel
            {
                GameID = game.ID,
                StoreID = store.ID,
                Expired = false,
                ExpiringDate = DateTime.Now,
                DatePosted = DateTime.Now,
                IsFree = true,
                Price = 32.4m,
                PreviousPrice = 43.2m,
                LimitedTimeDeal = true,
                URL = @"https://www.youtube.com/watch?v=O901ZEBQ230&t=4s&ab_channel=ApnaJ"
            };


            var adDB = await GameBusinessAddition.AddDealAsync(deal);

            // even if a store has been added even if it exist then update the info
            Assert.True(adDB != 0);



        }


        [Fact]
        public async Task AddDeal_ShouldAddDealsShouldReturn0WhenInvalidDataISPassed()
        {

            var deal = new DealModel
            {
                GameID = 32,
                StoreID = 32,
                Expired = false,
                ExpiringDate = DateTime.Now,
                DatePosted = DateTime.Now,
                IsFree = true,
                Price = 32.4m,
                PreviousPrice = 43.2m,
                LimitedTimeDeal = true,
               
            };


            var adDB = await GameBusinessAddition.AddDealAsync(deal);

            // even if a store has been added even if it exist then update the info
            Assert.True(adDB == 0);



        }



        [Fact]
        public async Task AddTag_ShouldReturnTheIDWhenGameTagAlreadyExists()
        {
          


            var p = new Tag { Title = _testTag };
            p.ID = await GameBusinessAddition.AddTagAsync(p);


            // even if a store has been added even if it exist then update the info
            Assert.True(p.ID != 0);

        }


        [Fact]
        public async Task AddGameTag_ShouldReturnTheIDWhenGameTagAlreadyExists()
        {
            var game = await GameBusinessAccess.GetGameByTitleAsync(_testGameTitle2);
            var tag = await GameBusinessAccess.GetTagByTitleAsync(_testTag);

            var gameTag = new GameTagDetailsModel { GameID = game.ID, TagID = tag.ID };

            var addGameWithSteamModel = await GameBusinessAddition.AddGameTagDetailsAsync(gameTag);

            Assert.True(addGameWithSteamModel != 0);

        }


        [Fact]
        public async Task AddSystemRequirement_ShouldReturn0IfGameAndPlatformIDDontExistONDB()
        {
            var game = await GameBusinessAccess.GetGameByTitleAsync(_testGameTitle2);
            var platform = await GameBusinessAccess.GetPlatformByTitleAsync(_testPlatform);

            var sr = new SystemRequirement
            {
                GameID = game.ID,
                PlatformID = platform.ID,
                Memory = "5gb",
                MinimumSystemRequirement = true,
                Storage = "43gb",
                Os = "windows",
                Processor = "I7",
                Requirement ="hello"
                
            };

            var t = await GameBusinessAddition.AddSystemRequirementAsync(sr);

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
                Requirement = "hello2"

            };

            var sr2AddDB = await GameBusinessAddition.AddSystemRequirementAsync(sr2);

            Assert.Equal(0, sr2AddDB);

        }




    }
}
