using Dapper;
using DataAccessLibrary.Factories;
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

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryAsync<ReleaseDateModel>(query);
            }
        }

        public static async Task<ReleaseDateModel> GetReleaseDateByIdAsync(int id)
        {

            string query = $@"SELECT * FROM release_date rd WHERE rd.release_date_id=@ReleaseDateId";

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryFirstOrDefaultAsync<ReleaseDateModel>(query, new { ReleaseDateId = id });
            }

        }

    }
}
