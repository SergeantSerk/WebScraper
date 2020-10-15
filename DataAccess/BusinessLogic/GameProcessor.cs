
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interface;
using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLibrary.BusinessLogic
{
    public static class GameProcessor
    {
       

        //public  static List<FullGameModel> GetAllGames()
        //{
        //    string sqlQuery = $@"SELECT * 
        //    FROM Game g 
        //    LEFT JOIN Steamdetails sd ON g.SteamDetailsID = sd.ID 
        //    LEFT JOIN SystemRequirement sr ON sr.GameID = g.ID
        //         LEFT JOIN Platform p ON sr.PlatformID = p.ID
        //    LEFT JOIN GameTagDetails gt ON gt.GameID = g.ID
        //         LEFT JOIN Tag t ON gt.TagID = t.ID
        
        //   ";

        //    return SqlDataAccess.GetFullData(sqlQuery);

        //}


        public static List<FullGameModel> GetAllGames()
        {
            string sqlQuery = $@"SELECT * FROM Game;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement;
                         SELECT * FROM Platform;

           ";

            return SqlDataAccess.GetFullData(sqlQuery);

        }


        public static GameModel GetGameById(int id)
        {
            //  join game with steam detail
            string sqlQuery = $@"SELECT  ga*,sd* FROM Game ga LEFT JOIN Steamdetails sd on ga.SteamDetailsID = sd.ID WHERE ID = @ID ";

            var t = SqlDataAccess.GetData<GameModel>(sqlQuery, new GameModel { ID = id });

            return t.First();

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
