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
    public class DBAccess
    {

        private  static string GetConnectionString(string connectionName = "Default")
        {
            var configuration = Factory.getConfiguration();

            return configuration.GetConnectionString(connectionName);
        }

       

        protected static async Task<List<FullGameModel>> GetFullGameData(string query, Object param = null)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                using (var lists = await connection.QueryMultipleAsync(query, param))
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

                    foreach (var game in fullGameModels)
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


    
        protected static async Task<int> SaveDataAsync<T>(string query, T data)
        {


            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    int index;

                    try
                    {
                        index = await connection.ExecuteScalarAsync<int>(query, data, trans);

                        
                        trans.Commit();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"{e}");
                        trans.Rollback();
                        index = 0;
                    }



                    return index;
                }


            }
        }


        protected static int DeleteData<T>(string query, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return connection.Execute(query, data);
            }
        }




        protected static async Task<List<T>> GetData<T>(string query, T param)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var game = await connection.QueryAsync<T>(query, param);

                return game.ToList();
            }
        }


        protected static async Task<FullGameModel> GetGameModelData(string query, object param)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var game = await connection.QueryAsync<FullGameModel, SteamDetailsModel, FullGameModel>
                    (query, (g, sd) => { g.SteamDetail = sd; return g; }, param);

                return game.FirstOrDefault();
            }

        }

        protected static async Task<GameTagDetailsModel> GetGameTagDetialsData(string query, object param)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var gameTag = await connection.QueryAsync<GameTagDetailsModel, Tag, GameTagDetailsModel>
                    (query, (g, t) => { g.Tag = t; return g; }, param);

                return gameTag.FirstOrDefault();
            }

        }


    }
}
