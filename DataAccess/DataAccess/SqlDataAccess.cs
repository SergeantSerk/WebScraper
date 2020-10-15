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

        public static List<FullGameModel> GetFullData(string query)
        {
          

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                
                using (var lists = connection.QueryMultiple(query))
                {
                    var fullGameModels = lists.Read<FullGameModel>().ToList();
                    // convert to  Lookup<TKey,TElement> set key for looking up
                    var steamDetails = lists.Read<SteamDetailsModel>().ToLookup(sd => sd.ID);
                   
                   
                    var systemRequirements = lists.Read<SystemRequirement>().ToList();

                    var platforms = lists.Read<Platform>().ToLookup(p => p.ID);

                    // set up the platform
                    systemRequirements.ForEach(sr => sr.Platform = platforms[sr.PlatformID].FirstOrDefault());

                    var srLookUp = systemRequirements.ToLookup(sr => sr.GameID);

                    foreach(var game in fullGameModels)
                    {
                        game.SystemRequirements = srLookUp[game.ID].ToList();
                        game.SteamDetail = steamDetails[game.SteamDetailsID].FirstOrDefault();
                    }


                    return fullGameModels;
                }

         

                //foreach (var game in fullGameModels)
                //{
                //    if(steamDetails.Count > 0)
                //    steamDetails.Where(s => s.ID == game.SteamDetailsID).First();

                //    game.SystemRequirements = systemRequirements.Where(sr => sr.GameID == game.ID).ToList();
                   

                //}


               
            }
        }

        public static  int SaveData<T>(string query, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return connection.ExecuteScalar<int>(query, data);
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
