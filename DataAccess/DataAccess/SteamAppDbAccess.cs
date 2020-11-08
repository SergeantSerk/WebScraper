using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using System.Collections.Generic;

using System.Threading.Tasks;
namespace DataAccessLibrary.DataAccess
{
    public class SteamAppDbAccess : DBAccessAbstraction, ISteamAppDbAccess
    {
        public async Task<int> AddSteamAppAsync(SteamAppAddModel steamApp)
        {
            string query = @"INSERT INTO steamapp (SteamAppId, SteamReview, SteamReviewCount, Valid) 
                            OUTPUT INSERTED.SteamAppId
                                   VALUES(@SteamAppId, @SteamReview, @SteamReviewCount, @Valid)";

            return await SaveDataAsync(query, steamApp);
        }

        public async Task<IEnumerable<SteamAppModel>> GetAllSteamAppsAsync()
        {
            var query = "SELECT * FROM steamapp";

            return await GetAllDataAsync<SteamAppModel>(query);
        }

        public async Task<SteamAppModel> GetSteamAppByIdAsync(int id)
        {

            string query = "SELECT * FROM steamapp sa WHERE sa.SteamAppId=@SteamAppId";

            return await GetSingleDataAsync<SteamAppModel>(query, new { SteamAppId = id });



        }
    }
}
