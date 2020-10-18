
using Dapper;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interface;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
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
           

            return await GetGameByIdAsync(id);

        }

        public static async Task<GameModel> GetGameByTitleAsync(string title)
        {
           

            return await GetGameByTitleAsync(title); ;

        }

        public static async Task<Tag> GetTagByTitleAsync(string title)
        {
            //  join game with steam detail
            string sqlQuery = $@"SELECT * FROM Tag WHERE Title =@Title;";
            var t = await SqlDataAccess.GetDataAsync(sqlQuery, new Tag { Title = title });

            return t.FirstOrDefault();

        }


     

        public static async Task<SteamDetailsModel> GetSteamDetailsByIdAsync(int id)
        {
            string query = $@"Select * FROM Steamdetails WHERE ID = @ID;";

           var data =  await SqlDataAccess.GetDataAsync<SteamDetailsModel>(query, new SteamDetailsModel { ID = id });

            return data.FirstOrDefault();

        }

        public static async Task<SteamDetailsModel> GetSteamDetailsBySteamIdAsync(string steamID)
        {
            string query = $@"Select * FROM Steamdetails WHERE SteamID = @steamID;";

            var data = await SqlDataAccess.GetDataAsync<SteamDetailsModel>(query, new SteamDetailsModel { SteamID = steamID });

            return data.FirstOrDefault();

        }


        public static async Task<Platform> GetPlatformByTitleAsync(string platformTitle)
        {
            string query = $@"Select * FROM Platform WHERE title = @Title;";

            var data = await SqlDataAccess.GetDataAsync(query, new Platform { Title = platformTitle });

            return data.FirstOrDefault();

        }


        public static async Task<StoreModel> GetStoreByNameAsync(string storeName)
        {
            string query = $@"Select * FROM Store WHERE Name = @Name;";

            var data = await SqlDataAccess.GetDataAsync(query, new StoreModel { Name = storeName });

            return data.FirstOrDefault();

        }

        public static async Task<int> AddMediaAsync(IMediaModel media)
        {
            string query = $@"INSERT INTO Media (GameID,Url)  
                                    VALUES (@GameID, @Url) SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, media);

            return data;

        }



        public static async Task<int> AddStoreAsync(IStoreModel store)
        {
            string query = $@"IF NOT EXISTS ( SELECT ID FROM Store WHERE Name = @Name)
                    BEGIN INSERT INTO Store (Name,Logo) VALUES (@Name, @Logo) SELECT SCOPE_IDENTITY() END
                    ELSE BEGIN UPDATE Store SET Logo = @Logo OUTPUT INSERTED.ID
                    WHERE Name=@Name END;
                    ";

   
            var data = await SqlDataAccess.SaveDataTransaction(query, store);

            return data;

        }


        public static async Task<int> AddPlatformAsync(IPlatform platform)
        {
            string query = $@"
            IF NOT EXISTS ( SELECT ID FROM Platform WHERE Title = @Title)
             BEGIN INSERT INTO Platform (Title)  VALUES (@Title) SELECT SCOPE_IDENTITY() END
            ELSE SELECT ID FROM Platform WHERE Title = @Title";

            var data = await SqlDataAccess.SaveDataAsync(query, platform);

            return data;

        }


        public static async Task<int> AddGameTagDetailsAsync(IGameTagDetails gameTag)
        {
            string query = $@"IF NOT EXISTS ( SELECT ID FROM GameTagDetails 
                            WHERE GameID = @GameID AND TagID = @TagID)
                            BEGIN INSERT INTO GameTagDetails (GameID, TagID) 
                                VALUES (@GameID, @TagID) SELECT SCOPE_IDENTITY() END
                            ELSE SELECT ID FROM GameTagDetails 
                            WHERE GameID = @GameID AND TagID = @TagID";

            var data = await SqlDataAccess.SaveDataAsync(query, gameTag);

            return data;

        }
        public static async Task<int> AddGameTagDetailsAsync(List<GameTagDetailsModel> gameTag)
        {
            string query = $@"IF NOT EXISTS ( SELECT ID FROM GameTagDetails 
                            WHERE GameID = @GameID AND TagID = @TagID)
                            BEGIN INSERT INTO GameTagDetails (GameID, TagID) 
                                VALUES (@GameID, @TagID) SELECT SCOPE_IDENTITY() END
                            ELSE SELECT ID FROM GameTagDetails 
                            WHERE GameID = @GameID AND TagID = @TagID";

            var data = await SqlDataAccess.SaveDataAsync(query, gameTag);

            return data;

        }


        public static async Task<int> AddTagAsync(ITag tag)
        {
            string query = $@"IF NOT EXISTS ( SELECT ID FROM Tag WHERE Title = @Title)
                            BEGIN INSERT INTO Tag  VALUES (@Title) SELECT SCOPE_IDENTITY() END
                            ELSE SELECT ID FROM Tag WHERE Title=@Title";

            var data = await SqlDataAccess.SaveDataAsync(query, tag);

            return data;

        }



        public static async Task<int> AddDealAsync(IDealModel deal)
        {
            string query = $@"INSERT INTO Deal 
                (GameID,StoreID, Price, PreviousPrice, Expired, ExpiringDate, DatePosted, LimitedTimeDeal, Url, IsFree)  
                VALUES (@GameID,@StoreID, @Price, @PreviousPrice, @Expired, @ExpiringDate, 
                @DatePosted, @LimitedTimeDeal, @Url, @IsFree) 
                SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, deal);

            return data;

        }


        public static async Task<int> AddSystemRequirementAsync(ISystemRequirement systemRequirement)
        {
            

            string query = $@"INSERT INTO SystemRequirement 
                (GameID,PlatformId, Requirement, Processor, Os, Memory, Storage)  
                VALUES (@GameID,@PlatformId, @Requirement, @Processor, @Os, @Memory, @Storage) 
                SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, systemRequirement);

            return data;

        }
        public static async Task<int> AddGameAsync(GameModel game)
        {

            string query = $@" IF NOT EXISTS ( SELECT Title FROM Game WHERE Title = @Title)
                BEGIN INSERT INTO Game (About, Developer, Publisher, ReleaseDate,  Thumbnail, Title)  
                VALUES (@About, @Developer, @Publisher, @ReleaseDate, @Thumbnail, @Title) 
                SELECT SCOPE_IDENTITY() END ELSE BEGIN SELECT ID FROM Game WHERE Title=@Title END";

            var data = await SqlDataAccess.SaveDataAsync(query, game);

            return data;

        }

        public static async Task<int> AddSteamGameAsync(GameModel game)
        {
            
            if(game.SteamDetailsID == 0)
            {
                throw new InvalidDataException("SteamDetails ID cannot be 0");
            }
          

            string query = $@" IF NOT EXISTS ( SELECT Title FROM Game WHERE Title = @Title)
                BEGIN INSERT INTO Game (About, Developer, Publisher, ReleaseDate,  Thumbnail, Title, SteamDetailsID)  
                VALUES (@About, @Developer, @Publisher, @ReleaseDate, @Thumbnail, @Title,@SteamDetailsID) 
                SELECT SCOPE_IDENTITY() END ELSE SELECT ID FROM Game Where Title=@Title";

            var data = await SqlDataAccess.SaveDataAsync(query, game);

            return data;

        }

        public static async Task<int> AddSteamDetailsAsync(ISteamDetailsModel steamDetailsModel)
        {
            string query = $@"  IF NOT EXISTS ( SELECT ID FROM SteamDetails WHERE SteamID = @SteamID)   
                               BEGIN INSERT INTO Steamdetails (SteamID, SteamReview, SteamReviewCount) 
                               VALUES (@SteamID, @SteamReview, @SteamReviewCount) SELECT SCOPE_IDENTITY() END
                                ELSE  BEGIN UPDATE SteamDetails SET SteamReview = @SteamReview, SteamReviewCount =@SteamReviewCount
                                    OUTPUT INSERTED.ID WHERE SteamID = @SteamID END";

            var data = await SqlDataAccess.SaveDataAsync(query, steamDetailsModel);

            return data;

        }

        public static async Task<int> AddDealAsync(DealModel deal)
        {
            string query = $@"  
                    IF NOT EXISTS ( SELECT ID FROM Deal WHERE GameID = @GameID AND StoreID = @StoreID AND Url =@URL)
                            BEGIN INSERT INTO Deal (GameID, StoreID, Price, PreviousPrice, 
                              Expired, ExpiringDate,DatePosted, LimitedTimeDeal, Url, IsFree ) 
                             VALUES (@GameID, @StoreID, @Price, @PreviousPrice, 
                             @Expired,@ExpiringDate,@DatePosted, @LimitedTimeDeal, 
                              @Url, @IsFree) SELECT SCOPE_IDENTITY() END
                          ELSE BEGIN SELECT ID FROM Deal WHERE GameID = @GameID AND StoreID = @StoreID AND Url =@URL END";

            var data = await SqlDataAccess.SaveDataAsync(query, deal);

            return data;
        }


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
