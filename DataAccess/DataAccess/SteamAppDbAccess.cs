﻿using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.DataAccess.DBAccessFactory;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace DataAccessLibrary.DataAccess
{
    public class SteamAppDbAccess : DBAccessAbstraction, ISteamAppDbAccess
    {
        public async Task<IEnumerable<SteamAppModel>> GetAllSteamAppsAsync()
        {
            var query = $"SELECT * FROM steamapp";

            return await GetAllDataAsync<SteamAppModel>(query);
        }

        public async Task<SteamAppModel> GetSteamAppByIdAsync(int id)
        {

            string query = $@"SELECT * FROM steamapp sa WHERE sa.steamapp_id=@SteamAppId";

            return await GetSingleDataAsync<SteamAppModel>(query, new { SteamAppId = id });



        }
    }
}
