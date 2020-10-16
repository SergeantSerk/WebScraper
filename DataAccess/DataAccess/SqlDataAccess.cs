using Dapper;
using DataAccessLibrary.Factories;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public static class SqlDataAccess
    {

        private  static string GetConnectionString(string connectionName = "Default")
        {
            var configuration = Factory.getConfiguration();

            return configuration.GetConnectionString(connectionName);
        }

        public  static List<T> GetData<T>(string query)
        {
           
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return connection.Query<T>(query).ToList();
            }
        }

  

        public static List<T> GetData<T>(string query, T data)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return connection.Query<T>(query, data).ToList();
            }
        }

        public static async Task<List<T>> GetDataAsync<T>(string query)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var data = await connection.QueryAsync<T>(query);

                return data.ToList();
            }
        }

        public static async Task<List<T>> GetDataAsync<T>(string query, T param)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var data = await connection.QueryAsync<T>(query, param);

                return data.ToList();
            }
        }

        //// return all games including their inner table
        //public static List<FullGameModel> GetFullData(string query)
        //{

        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        // used to filter out dup
        //        var fullGameDictionary = new Dictionary<int, FullGameModel>();
        //        var SystemRequirementDic = new Dictionary<int, SystemRequirement>();
        //        var gameTagDetailsDic = new Dictionary<int, GameTagDetails>();


        //         connection.Query<
        //            FullGameModel, SteamDetailsModel, SystemRequirement,
        //            Platform, GameTagDetails, Tag,FullGameModel>
        //            (query,
        //            (game, steamdetail, systemRequirement, platform, gameTagDetails, tag) =>
        //            {


        //                FullGameModel gameEntry;

        //                // check if an full game model already exist in the dictonary
        //                if (!fullGameDictionary.TryGetValue(game.ID, out gameEntry))
        //                {
        //                    gameEntry = game;

        //                    //game.GameTagDetails = new List<GameTagDetails>();
        //                    gameEntry.SystemRequirements = new List<SystemRequirement>();
        //                    gameEntry.GameTagDetails = new List<GameTagDetails>();

        //                    // add to dictonary
        //                    fullGameDictionary.Add(gameEntry.ID, gameEntry);
        //                }

        //                gameEntry.SteamDetail = steamdetail;
        //                // prevent duplicates
        //                SystemRequirement systemRequirementEntry;

        //                if (!SystemRequirementDic.TryGetValue(systemRequirement.ID, out systemRequirementEntry))
        //                {

        //                    systemRequirementEntry = systemRequirement;

        //                    systemRequirementEntry.Platform = platform;


        //                    // add to dictonary
        //                    SystemRequirementDic.Add(systemRequirementEntry.ID, systemRequirementEntry);

        //                    gameEntry.SystemRequirements.Add(systemRequirementEntry);
        //                }


        //                GameTagDetails gameTagDetailsEntry;

        //                if (!gameTagDetailsDic.TryGetValue(gameTagDetails.ID, out gameTagDetailsEntry))
        //                {

        //                    gameTagDetailsEntry = gameTagDetails;

        //                    gameTagDetailsEntry.Tag = tag;


        //                    // add to dictonary
        //                    gameTagDetailsDic.Add(gameTagDetailsEntry.ID, gameTagDetailsEntry);

        //                    gameEntry.GameTagDetails.Add(gameTagDetailsEntry);
        //                }



        //                return gameEntry;

        //            }).AsQueryable();
        //        return fullGameDictionary.Values.ToList();
        //    }
        //}

        public static List<FullGameModel> GetFullData(string query, object param = null)
        {
          

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                
                using (var lists = connection.QueryMultiple(query, param))
                {
                    var fullGameModels = lists.Read<FullGameModel>().ToList();
                    // convert to  Lookup<TKey,TElement> set key for looking up
                    var steamDetails = lists.Read<SteamDetailsModel>().ToLookup(sd => sd.ID);
                   
                   
                    var systemRequirements = lists.Read<SystemRequirement>().ToList();

                    var platforms = lists.Read<Platform>().ToLookup(p => p.ID);

                    var gameTagDetails = lists.Read<GameTagDetailsModel>().ToList();

                    var tag = lists.Read<Tag>().ToLookup(t => t.ID);

                    var stores = lists.Read<StoreModel>().ToLookup(s => s.ID);

                    var deals = lists.Read<DealModel>().ToList();

                    var media = lists.Read<MediaModel>().ToLookup(m => m.GameId);

                    deals.ForEach(d => d.Store = stores[d.StoreID].FirstOrDefault());
                    gameTagDetails.ForEach(gtd => gtd.Tag = tag[gtd.TagID].FirstOrDefault());


                    // set up the platform
                    systemRequirements.ForEach(sr => sr.Platform = platforms[sr.PlatformID].FirstOrDefault());

                    var gtdLookup = gameTagDetails.ToLookup(gtd => gtd.GameID);
                    var srLookUp = systemRequirements.ToLookup(sr => sr.GameID);
                    var dealLookup = deals.ToLookup(d => d.GameID);

                    foreach(var game in fullGameModels)
                    {
                        game.SystemRequirements = srLookUp[game.ID].ToList();
                        game.SteamDetail = steamDetails[game.SteamDetailsID].FirstOrDefault();
                        game.GameTagDetails = gtdLookup[game.ID].ToList();
                        game.Deals = dealLookup[game.ID].ToList();
                        game.Medias = media[game.ID].ToList();
                    }


                    return fullGameModels;
                }


            }
        }



        //public static int SaveData<T>(string query, T data)
        //{


        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        var execution = connection.Execute(query, data);



        //            return execution;

        //    }
        //}

        public static async Task<int> SaveDataAsync<T>(string query, T data)
        {


            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                


                

                    return await connection.ExecuteScalarAsync<int>(query, data);


           
            }
        }


        public static int DeleteData<T>(string query, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return connection.Execute(query, data);
            }
        }
    }
}
