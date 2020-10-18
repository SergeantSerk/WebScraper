
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
            var gameID = media.GameId;

            if (gameID != 0 && !String.IsNullOrEmpty(media.Url))
            {
                var game = await SqlDataAccess.GetGameByIdAsync(gameID);

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
          

            if(DataValidatorHelper.HasAllEmptyProperties(store))
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
            var name = platform.Title;
            if(!String.IsNullOrEmpty(name))
            {
                var plf = await SqlDataAccess.GetPlatformByTitleAsync(name);

                if(plf != null)
                {
                    return plf.ID;
                }

                return await SqlDataAccess.AddPlatformAsync(platform);
            }

            return 0;

        }


        public static async Task<int> AddGameTagDetailsAsync(GameTagDetailsModel gameTagDetails)
        {
            

            var gameID = gameTagDetails.GameID;

            if(IsNumberIDValid(gameID) && IsNumberIDValid(gameTagDetails.TagID))
            {

                var gtd = await SqlDataAccess.GetGameTagDetailsByGameIdAndTagIDAsync
                    (gameID, gameTagDetails.TagID);

                if (gtd == null)
                {

                    var game = await SqlDataAccess.GetGameByIdAsync(gameID);
                    var tag = await SqlDataAccess.GetTagByIDAsync(gameTagDetails.TagID);

                    if (tag != null && game != null)
                    {

                        var data = await SqlDataAccess.AddGameTagDetailsAsync(gameTagDetails);

                        return data;

                    }
                }

                return gtd.ID;

            }


            return 0;
        }


        private static bool IsNumberIDValid(int id)
        {
            return id == 0 ? false : true;
        }


        public static async Task<int> AddTagAsync(Tag tag)
        {
            if (!String.IsNullOrEmpty(tag.Title))
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


        //public static async Task<int> AddSystemRequirementAsync(ISystemRequirement systemRequirement)
        //{


        //    string query = $@"INSERT INTO SystemRequirement 
        //        (GameID,PlatformId, Requirement, Processor, Os, Memory, Storage)  
        //        VALUES (@GameID,@PlatformId, @Requirement, @Processor, @Os, @Memory, @Storage) 
        //        SELECT SCOPE_IDENTITY();";

        //    var data = await SqlDataAccess.SaveDataAsync(query, systemRequirement);

        //    return data;

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

        //public static async Task<int> AddSteamDetailsAsync(ISteamDetailsModel steamDetailsModel)
        //{
        //    string query = $@"  IF NOT EXISTS ( SELECT ID FROM SteamDetails WHERE SteamID = @SteamID)   
        //                       BEGIN INSERT INTO Steamdetails (SteamID, SteamReview, SteamReviewCount) 
        //                       VALUES (@SteamID, @SteamReview, @SteamReviewCount) SELECT SCOPE_IDENTITY() END
        //                        ELSE  BEGIN UPDATE SteamDetails SET SteamReview = @SteamReview, SteamReviewCount =@SteamReviewCount
        //                            OUTPUT INSERTED.ID WHERE SteamID = @SteamID END";

        //    var data = await SqlDataAccess.SaveDataAsync(query, steamDetailsModel);

        //    return data;

        //}

        //public static async Task<int> AddDealAsync(DealModel deal)
        //{
        //    string query = $@"  
        //            IF NOT EXISTS ( SELECT ID FROM Deal WHERE GameID = @GameID AND StoreID = @StoreID AND Url =@URL)
        //                    BEGIN INSERT INTO Deal (GameID, StoreID, Price, PreviousPrice, 
        //                      Expired, ExpiringDate,DatePosted, LimitedTimeDeal, Url, IsFree ) 
        //                     VALUES (@GameID, @StoreID, @Price, @PreviousPrice, 
        //                     @Expired,@ExpiringDate,@DatePosted, @LimitedTimeDeal, 
        //                      @Url, @IsFree) SELECT SCOPE_IDENTITY() END
        //                  ELSE BEGIN SELECT ID FROM Deal WHERE GameID = @GameID AND StoreID = @StoreID AND Url =@URL END";

        //    var data = await SqlDataAccess.SaveDataAsync(query, deal);

        //    return data;
        //}


        //public static int AddGame(IGameModel game)
        //{
        //    //    // ignore duplicate if it exists
        ////    string sqlQuery = @"INSERT IGNORE INTO Game 
        ////SELECT  @ReleaseDate, @About, @Thumbnail, @Title, @Developer, @Publisher, @SteamDetailsId
        ////WHERE NOT EXISTS (SELECT Title From Game WHERE Title = @Title); SELECT SCOPE_IDENTITY() ";

        //    // ignore duplicate if it exists
        //    string sqlQuery = @"
        //                   DECLARE @SDID int;
        //                   DECLARE @GID INT;
        //                   DECLARE @PID INT;
        //                   INSERT INTO SteamDetails VALUES (@SteamID, @SteamReview, @SteamReviewCount)
        //                   SELECT @SDID = scope_identity();
        //                   INSERT INTO Game OUTPUT inserted.ID VALUES ( @ReleaseDate, @About, @Thumbnail, 
        //                   @Title, @Developer, @Publisher, @SDID)
        //                   SELECT @GID = SCOPE_IDENTITY();
        //                   INSERT INTO Platform VALUES(@Title)
        //                   SELECT @PID = SCOPE_IDENTITY();
        //                   INSERT INTO SystemRequirement VALUES ( @GID, @PID, @Os,@Processor,@Memory,@Storage)

        //                ";

        //    var p = new DynamicParameters();

        //    p.Add("SDID", 0, DbType.Int32, ParameterDirection.Output);
        //    p.Add("GID", 0, DbType.Int32, ParameterDirection.Output);
        //    p.Add("PID", 0, DbType.Int32, ParameterDirection.Output);
        //    p.Add("@SteamID", game.SteamDetailsID);


        //    return SqlDataAccess.SaveData(sqlQuery, p);

        //}



    }
    }
