using Dapper;
using DataAccessLibrary.Factories;
using DataAccessLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace DataAccessLibrary.DataAccess
{
    public static class SteamAppDbAccess
    {
        public static async Task<IEnumerable<SteamAppModel>> GetAllSteamAppsAsync()
        {
            var query = $"SELECT * FROM steamapp";

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryAsync<SteamAppModel>(query);
            }
        }

        public static async Task<SteamAppModel> GetGameByIdAsync(int id)
        {

            string query = $@"SELECT * FROM steamapp sa WHERE sa.steamapp_id=@SteamAppId";

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryFirstOrDefaultAsync<SteamAppModel>(query, new { SteamAppId = id });
            }

        }
    }
}
