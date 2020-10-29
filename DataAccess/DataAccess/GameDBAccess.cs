using Dapper;
using DataAccessLibrary.Factories;
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

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryAsync<GameModel>(query);
            }
        }


        public static async Task<GameModel> GetGameByIdAsync(int id)
        {
          
            string query = $@"SELECT * FROM game g WHERE g.game_id=@GameId";

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryFirstOrDefaultAsync<GameModel>(query, new { GameId=id});
            }

        }



    }
}
