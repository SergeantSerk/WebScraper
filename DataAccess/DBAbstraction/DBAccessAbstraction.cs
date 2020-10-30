using Dapper;
using DataAccessLibrary.DataAccess.DBAccessFactory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess.Abstraction
{
    public abstract class DBAccessAbstraction
    {


        private string GetConnectionString(string connectionName = "Default")
        {
            var configuration = DALFactory.getConfiguration();

            return configuration.GetConnectionString(connectionName);
        }

        protected async Task<T> GetSingleDataAsync<T>(string query, object param)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return await connection.QueryFirstOrDefaultAsync<T>(query, param);
            }

        }

        protected async Task<IEnumerable<T>> GetAllDataAsync<T>(string query)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return await connection.QueryAsync<T>(query);
            }
        }


        protected async Task<int> SaveDataAsync(string query, object param)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return await connection.ExecuteScalarAsync<int>(query, param);
            }

        }
    }
}
