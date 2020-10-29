using Dapper;
using DataAccessLibrary.DataAccess.DBAccessFactory;
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

            return await DBFactory.GetAllDataAsync<SteamAppModel>(query);
        }

        public static async Task<SteamAppModel> GetSteamAppByIdAsync(int id)
        {

            string query = $@"SELECT * FROM steamapp sa WHERE sa.steamapp_id=@SteamAppId";

            return await DBFactory.GetSingleDataAsync<SteamAppModel>(query, new { SteamAppId = id });

        

        }
    }
}
