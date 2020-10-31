using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabaseUpdateModels;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class ReleaseDateDBAccess :  DBAccessAbstraction, IReleaseDateDBAccess
    {
        public async Task<int> AddReleaseDateAsync(ReleaseDateAddModel releaseDate)
        {

            string query = @"INSERT INTO ReleaseDate (ComingSoon, ReleasedDate) 
                            OUTPUT INSERTED.ReleaseDateId
                                   VALUES(@ComingSoon, @ReleasedDate)";

            return await SaveDataAsync(query, releaseDate);
        }

        public async Task<IEnumerable<ReleaseDateModel>> GetAllReleaseDateAsync()
        {
            var query = $"SELECT * FROM ReleaseDate";

            return await GetAllDataAsync<ReleaseDateModel>(query);
        }

        public async Task<ReleaseDateModel> GetReleaseDateByIdAsync(int id)
        {

            string query = $@"SELECT * FROM ReleaseDate rd WHERE rd.ReleaseDateId=@ReleaseDateId";

            return await GetSingleDataAsync<ReleaseDateModel>(query, new { ReleaseDateId = id });

        }

        public async void UpdateReleaseDateAsync(ReleaseDateUpdateModel releaseDate)
        {

            string query = @"UPDATE ReleaseDate SET ComingSoon = @ComingSoon, ReleasedDate= @ReleasedDate) 
                           WHERE  ReleaseDateId=@ReleaseDateId";


           await SaveDataAsync(query, releaseDate);
        }
    }
}
