using Dapper;
using DataAccessLibrary.DataAccess.DBAccessFactory;
using DataAccessLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public static class GameDBAccess
    {
        public static async Task<IEnumerable<GameModel>> GetAllGamesAsync()
        {
            var query = $"SELECT * FROM game";

            return await DBFactory.GetAllDataAsync<GameModel>(query);
        }


        public static async Task<GameModel> GetGameByIdAsync(int id)
        {
          
            string query = $@"SELECT * FROM game g WHERE g.game_id=@GameId";

            return await DBFactory.GetSingleDataAsync<GameModel>(query, new { GameId = id });

           
        }



    }
}
