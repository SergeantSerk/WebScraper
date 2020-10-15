using DataAccessLibrary.BusinessLogic;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataAccessLibraryTest.BusinessLogicTest
{
    public class GameProcessTest
    {

        [Fact]
        public void AddGame()
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


            //// add games and return last row
            //var gameAdded = GameProcessor.AddGame(game);


            //var foundGame = GameProcessor.GetGameById(gameAdded);



           
         
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
    }
}
