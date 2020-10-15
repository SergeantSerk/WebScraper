
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interface;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLibrary.BusinessLogic
{
    public static class GameProcessor
    {
       


        public static List<FullGameModel> GetAllFullGames()
        {
            string sqlQuery = $@"SELECT * FROM Game;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal;
                         SELECT * FROM Media;

           ";

            return SqlDataAccess.GetFullData(sqlQuery);

        }


        public static FullGameModel GetFullGameById(int id)
        {
            //  join game with steam detail
            string sqlQuery = $@"SELECT * FROM Game WHERE ID =@ID;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement WHERE GameID = @ID;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails WHERE GameID = @ID;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal WHERE GameID = @ID;
                         SELECT * FROM Media WHERE GameID = @ID;

           ";
            var t = SqlDataAccess.GetFullData(sqlQuery, new GameModel { ID = id });

            return t.SingleOrDefault();

        }


        public static FullGameModel GetFullGameByTitle(string title)
        {
            //  join game with steam detail
            string sqlQuery = $@"SELECT * FROM Game WHERE Title =@Title;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement ;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails ;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal;
                         SELECT * FROM Media ;

           ";
            var t = SqlDataAccess.GetFullData(sqlQuery, new GameModel { Title = title });

            return t.SingleOrDefault();

        }


        public static int AddGame(IGameModel game)
        {
            // ignore duplicate if it exists
            string sqlQuery = @"INSERT INTO Game 
        SELECT  @ReleaseDate, @About, @Thumbnail, @Title, @Developer, @Publisher, @SteamDetailsId
        WHERE NOT EXISTS (SELECT Title From Game WHERE Title = @Title); SELECT SCOPE_IDENTITY() ";

            return SqlDataAccess.SaveData(sqlQuery, game);

        }

        //public static int DeleteGame(IGame game)
        //{

        //}

    }
}
