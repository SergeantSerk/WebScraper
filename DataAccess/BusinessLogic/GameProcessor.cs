
using Dapper;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interface;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities;
using DataAccessLibrary.Utilities.Models;
using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.BusinessLogic
{
    public static class GameProcessor
    {
       




        public static async Task<List<FullGameModel>> GetAllFullGames()
        {
          

            return await SqlDataAccess.GetAllFullGames();

        }

        public static async Task<FullGameModel> GetFullGameById(int id)
        {
            var game = await SqlDataAccess.GetFullGameByID(id);
            return game;
        }

        public static async Task<FullGameModel> GetFullGameByTitle(string title)
        {
            var g = await SqlDataAccess.GetFullGameByTitle(title);
            return g;

        }

        public static async Task<GameModel> GetGameByIdAsync(int id)
        {
            return await SqlDataAccess.GetGameByIdAsync(id);
        }

        public static async Task<GameModel> GetGameByTitleAsync(string title)
        {
            return await SqlDataAccess.GetGameByTitleAsync(title); ;
        }




        public static async Task<Tag> GetTagByTitleAsync(string title)
        {
       
            return await SqlDataAccess.GetTagByTitleAsync(title);

        }


        public static async Task<SteamDetailsModel> GetSteamDetailsByIdAsync(int id)
        {

            return await SqlDataAccess.GetSteamdetailsByIDAsync(id);

        }

        public static async Task<SteamDetailsModel> GetSteamDetailsBySteamIdAsync(string steamID)
        {

            return await SqlDataAccess.GetSteamdetailsBySteamIDAsync(steamID); 
        }


        public static async Task<Platform> GetPlatformByTitleAsync(string platformTitle)
        {
            
            return await SqlDataAccess.GetPlatformByTitleAsync(platformTitle);

        }


        public static async Task<StoreModel> GetStoreByNameAsync(string storeName)
        {

            return await SqlDataAccess.GetStoreByNameAsync(storeName);
        }


        public static async Task<List<MediaModel>> GetMediasByGameIdAsync(int gameID)
        {
            return await SqlDataAccess.GetMediasByGameIdAsync(gameID);
        }

        public static async Task<GameTagDetailsModel> GetGameTagDetailsByGameIdAndTagIDAsync(int gameID, int tagID)
        {
            return await SqlDataAccess.GetGameTagDetailsByGameIdAndTagIDAsync(gameID, tagID);
        }


        public static async Task<int>  AddMediaAsync(MediaModel media)
        {
            

            if (DataValidatorHelper.IsValid(media))
            {
                var game = await SqlDataAccess.GetGameByIdAsync(media.GameId);

                if (game != null)
                {
                    var checkMedia = await SqlDataAccess.GetMediasByGameIdAndUrlAsync(game.ID, media.Url);

                    if (checkMedia != null)
                    {
                        return checkMedia.ID;
                    }

                    return await SqlDataAccess.AddMediaAsync(media);
                }
            }

            return 0;
        }



        public static async Task<int> AddStoreAsync(StoreModel store)
        {
          

            if(DataValidatorHelper.IsValid(store))
            {
                var checkStore = await SqlDataAccess.GetStoreByNameAsync(store.Name);

                if(checkStore != null)
                {

                    return checkStore.ID;
                }

                return await SqlDataAccess.AddStoreAsync(store);

            }

            Console.WriteLine("Store had insufficient data to add to database");

            return 0;

        }


        public static async Task<int> AddPlatformAsync(Platform platform)
        {
          
            if(DataValidatorHelper.IsValid(platform))
            {
                var plf = await SqlDataAccess.GetPlatformByTitleAsync(platform.Title);

                if(plf != null)
                {
                    return plf.ID;
                }

                return await SqlDataAccess.AddPlatformAsync(platform);
            }

            return 0;

        }


        public static async Task<int> AddGameTagDetailsAsync(GameTagDetailsModel gtd)
        {
            

         

            if(DataValidatorHelper.IsValid(gtd))
            {

                var gtdDB = await SqlDataAccess.GetGameTagDetailsByGameIdAndTagIDAsync
                    (gtd.GameID, gtd.TagID);

                if (gtd == null)
                {

                    var game = await SqlDataAccess.GetGameByIdAsync(gtd.GameID);
                    var tag = await SqlDataAccess.GetTagByIDAsync(gtd.TagID);

                    if (tag != null && game != null)
                    {

                        var data = await SqlDataAccess.AddGameTagDetailsAsync(gtd);

                        return data;

                    }
                }

                return gtdDB.ID;

            }


            return 0;
        }






        public static async Task<int> AddTagAsync(Tag tag)
        {
            if (DataValidatorHelper.IsValid(tag))
            {
                var tagDB = await SqlDataAccess.GetTagByTitleAsync(tag.Title);

                if (tagDB == null)
                {
                
                    return await SqlDataAccess.AddTagAsync(tag);
                }

                return tagDB.ID;
            }

            return 0;

        }



        //public static async Task<int> AddDealAsync(IDealModel deal)
        //{
        //    string query = $@"INSERT INTO Deal 
        //        (GameID,StoreID, Price, PreviousPrice, Expired, ExpiringDate, DatePosted, LimitedTimeDeal, Url, IsFree)  
        //        VALUES (@GameID,@StoreID, @Price, @PreviousPrice, @Expired, @ExpiringDate, 
        //        @DatePosted, @LimitedTimeDeal, @Url, @IsFree) 
        //        SELECT SCOPE_IDENTITY();";

        //    var data = await SqlDataAccess.SaveDataAsync(query, deal);

        //    return data;

        //}


        public static async Task<int> AddSystemRequirementAsync(SystemRequirement sr)
        {
            
            if(DataValidatorHelper.IsValid(sr))
            {
                var game = await SqlDataAccess.GetGameByIdAsync(sr.GameID);
                var platform = await SqlDataAccess.GetPlatformByIDAsync(sr.PlatformID);

                if (game != null && platform != null)
                {

                    var srDB = await SqlDataAccess.GetSystemRequirementByGameIdAsync
                        (sr.GameID, sr.MinimumSystemRequirement, sr.PlatformID);

                    if (srDB == null)
                    {

                        return await SqlDataAccess.AddSystemRequirementAsync(sr);
                    }

                    return srDB.ID;
                }
            }


            return 0;
        }

        public static async Task<int> AddSteamDetailsAsync(SteamDetailsModel steamDetails)
        {
            if(DataValidatorHelper.IsValid(steamDetails))
            {
                var sdDB = await SqlDataAccess.GetSteamdetailsBySteamIDAsync(steamDetails.SteamID);

                if(sdDB == null)
                {
                    return await SqlDataAccess.AddSteamDetailsAsync(steamDetails);

                }
                return sdDB.ID;
            }

            return 0;

        }
        public static async Task<int> AddDealAsync(DealModel deal)
        {
            
            if(DataValidatorHelper.IsValid(deal))
            {

                var game = await SqlDataAccess.GetGameByIdAsync(deal.GameID);
                var store = await SqlDataAccess.GetStoreByIDAsync(deal.StoreID);

                if (game != null && store != null)
                {
                    var dealDB = await SqlDataAccess.

                    GetDealByGameIdAndStoreIDAsync(deal.GameID, deal.StoreID, deal.URL);

                    if (dealDB == null)
                    {
                        return await SqlDataAccess.AddDealAsync(deal);
                    }

                    return dealDB.ID;
                }
            }
            return 0;

        }


        

        //}
        //public static async Task<int> AddGameAsync(GameModel game)
        //{

        //    string query = $@" IF NOT EXISTS ( SELECT Title FROM Game WHERE Title = @Title)
        //        BEGIN INSERT INTO Game (About, Developer, Publisher, ReleaseDate,  Thumbnail, Title)  
        //        VALUES (@About, @Developer, @Publisher, @ReleaseDate, @Thumbnail, @Title) 
        //        SELECT SCOPE_IDENTITY() END ELSE BEGIN SELECT ID FROM Game WHERE Title=@Title END";

        //    var data = await SqlDataAccess.SaveDataAsync(query, game);

        //    return data;

        //}

        //public static async Task<int> AddSteamGameAsync(GameModel game)
        //{

        //    if(game.SteamDetailsID == 0)
        //    {
        //        throw new InvalidDataException("SteamDetails ID cannot be 0");
        //    }


        //    string query = $@" IF NOT EXISTS ( SELECT Title FROM Game WHERE Title = @Title)
        //        BEGIN INSERT INTO Game (About, Developer, Publisher, ReleaseDate,  Thumbnail, Title, SteamDetailsID)  
        //        VALUES (@About, @Developer, @Publisher, @ReleaseDate, @Thumbnail, @Title,@SteamDetailsID) 
        //        SELECT SCOPE_IDENTITY() END ELSE SELECT ID FROM Game Where Title=@Title";

        //    var data = await SqlDataAccess.SaveDataAsync(query, game);

        //    return data;

        //}

      



    }
}
