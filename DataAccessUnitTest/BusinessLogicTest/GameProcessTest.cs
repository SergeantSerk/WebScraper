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


            // add games and return last row
            var gameAdded = GameProcessor.AddGame(game);


            var foundGame = GameProcessor.GetGameById(gameAdded);



           
         
        }

        [Fact]
        public void GetFullGame()
        {

            var allGames = GameProcessor.GetAllGames();

        }
    }
}
