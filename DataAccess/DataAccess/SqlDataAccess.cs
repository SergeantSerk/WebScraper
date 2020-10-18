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

       

        private static async Task<List<FullGameModel>> GetFullGameData(string query, Object param = null)
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


    
        public static async Task<int> SaveDataAsync<T>(string query, T data)
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


        public static int DeleteData<T>(string query, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return connection.Execute(query, data);
            }
        }



        private static async Task<GameModel> GetGameModel(string query, object param)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var game = await connection.QueryAsync<GameModel, SteamDetailsModel, GameModel>
                    (query, (g, sd) => { g.SteamDetails = sd; return g; }, param);

                return game.FirstOrDefault();
            }

        }



        public static async Task<GameModel> GetGameByIdAsync(int id)
        {
            //  join game with steam detail
            string query = $@"SELECT * FROM Game g WHERE ID =@ID
                                 LEFT JOIN SteamDetails sd ON  g.SteamDetailsID = sd.ID";

           
            return await GetGameModel(query, new { ID =id });

        }

        public static async Task<GameModel> GetGameByTitleAsync(string title)
        {
            //  join game with steam detail
            string query = $@"SELECT * FROM Game WHERE Title =@Title;";
            

            return await GetGameModel(query, new { Title = title });

        }




        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public static async Task<List<FullGameModel>> GetAllFullGames()
        {
            string query = $@"SELECT * FROM Game;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal;
                         SELECT * FROM Media;

           ";


            return await GetFullGameData(query);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<FullGameModel> GetFullGameByID(int id)
        {
            string query = $@"SELECT * FROM Game WHERE ID =@ID;
                         Select * FROM Steamdetails  ;
                         SELECT * FROM SystemRequirement WHERE GameID = @ID;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails WHERE GameID = @ID;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal WHERE GameID = @ID;
                         SELECT * FROM Media WHERE GameID = @ID";

            var g = await GetFullGameData(query, new { ID = id });

            return g.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static async Task<FullGameModel> GetFullGameByTitle(string title)
        {
            string query = $@"SELECT * FROM Game WHERE Title =@Title;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement ;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails ;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal;
                         SELECT * FROM Media ;";


            var g = await GetFullGameData(query, new { Title = title });

            return g.FirstOrDefault();
        }

    }
}
