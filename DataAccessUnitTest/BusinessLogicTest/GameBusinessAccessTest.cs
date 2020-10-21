using DataAccessLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessLibraryTest.BusinessLogicTest
{
    public class GameBusinessAccessTest
    {



        [Fact]
        public async Task GetFullGameById_ShouldReturnTestData()
        {
            var allGames = await GameBusinessAccess.GetAllFullGames();

            var game = await GameBusinessAccess.GetFullGameById(allGames.FirstOrDefault().ID);


            Assert.NotNull(game);

        }


        [Fact]
        public async Task GetFullGameByTitleShouldReturnNullforInvalidGame()
        {

            var allGames = await GameBusinessAccess.GetFullGameByTitle("TestTitle1");


            Assert.Null(allGames);

        }

        [Fact]
        public async Task GetAllFullGame_ShouldReturnDealsThatAreNotExpired()
        {

            var expiredDealExist = false;

            var allGames = await GameBusinessAccess.GetAllFullGames();

            foreach (var game in allGames)
            {
                foreach (var deal in game.Deals)
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
    }
}
