﻿using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseModels;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class ReleaseDateDBAccess :  DBAccessAbstraction, IReleaseDateDBAccess
    {


        public async Task<IEnumerable<ReleaseDateModel>> GetAllReleaseDateAsync()
        {
            var query = $"SELECT * FROM release_date";

            return await GetAllDataAsync<ReleaseDateModel>(query);
        }

        public async Task<ReleaseDateModel> GetReleaseDateByIdAsync(int id)
        {

            string query = $@"SELECT * FROM release_date rd WHERE rd.release_date_id=@ReleaseDateId";

            return await GetSingleDataAsync<ReleaseDateModel>(query, new { ReleaseDateId = id });

        }

    }
}
