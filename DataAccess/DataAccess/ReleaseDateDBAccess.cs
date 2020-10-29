using Dapper;
using DataAccessLibrary.DataAccess.DBAccessFactory;
using DataAccessLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public static class ReleaseDateDBAccess
    {
       

        public static async Task<IEnumerable<ReleaseDateModel>> GetAllReleaseDateAsync()
        {
            var query = $"SELECT * FROM release_date";

            return await DBFactory.GetAllDataAsync<ReleaseDateModel>(query);
        }

        public static async Task<ReleaseDateModel> GetReleaseDateByIdAsync(int id)
        {

            string query = $@"SELECT * FROM release_date rd WHERE rd.release_date_id=@ReleaseDateId";

            return await DBFactory.GetSingleDataAsync<ReleaseDateModel>(query, new { ReleaseDateId = id });

        }

    }
}
