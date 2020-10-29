using Dapper;
using DataAccessLibrary.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess.DBAccessFactory
{
    public static class DBFactory
    {
        public static async Task<T> GetSingleDataAsync<T>(string query, object param)
        {

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryFirstOrDefaultAsync<T>(query, param);
            }

        }

        public static async Task<IEnumerable<T>> GetAllDataAsync<T>(string query)
        {

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.QueryAsync<T>(query);
            }
        }


        public static async Task<int> SaveDataAsync(string query, object param)
        {

            using (IDbConnection connection = new SqlConnection(Factory.GetConnectionString()))
            {

                return await connection.ExecuteScalarAsync<int>(query, param);
            }

        }

    }
}
