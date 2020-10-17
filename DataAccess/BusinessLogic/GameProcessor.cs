
using Dapper;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interface;
using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.BusinessLogic
{
    public static class GameProcessor
    {
       


        public static List<FullGameModel> GetAllFullGames()
        {
            string sqlQuery = $@"SELECT * FROM Game;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal;
                         SELECT * FROM Media;

           ";

            return SqlDataAccess.GetFullData(sqlQuery);

        }


        public static FullGameModel GetFullGameById(int id)
        {
            //  join game with steam detail
            string sqlQuery = $@"SELECT * FROM Game WHERE ID =@ID;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement WHERE GameID = @ID;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails WHERE GameID = @ID;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal WHERE GameID = @ID;
                         SELECT * FROM Media WHERE GameID = @ID;

           ";
            var t = SqlDataAccess.GetFullData(sqlQuery, new GameModel { ID = id });

            return t.SingleOrDefault();

        }


        public static FullGameModel GetFullGameByTitle(string title)
        {
            //  join game with steam detail
            string sqlQuery = $@"SELECT * FROM Game WHERE Title =@Title;
                         Select * FROM Steamdetails ;
                         SELECT * FROM SystemRequirement ;
                         SELECT * FROM Platform;
                         SELECT * FROM GameTagDetails ;
                         SELECT * FROM Tag;
                         SELECT * FROM Store;
                         SELECT * FROM Deal;
                         SELECT * FROM Media ;

           ";
            var t = SqlDataAccess.GetFullData(sqlQuery, new GameModel { Title = title });

            return t.SingleOrDefault();

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


        public static async Task<int> AddSteamDetailsAsync(ISteamDetailsModel steamDetailsModel)
        {
            string query = $@"INSERT INTO Steamdetails (SteamID, SteamReview, SteamReviewCount) 
                                    VALUES (@SteamID, @SteamReview, @SteamReviewCount) SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, steamDetailsModel);

            return data;

        }

        public static async Task<int> AddTagAsync(ITag tag)
        {
            string query = $@"INSERT INTO Tag  
                                    VALUES (@Title) SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, tag);

            return data;

        }



        public static async Task<int> AddPlatformAsync(IPlatform platform)
        {
            string query = $@"INSERT INTO Platform  
                                    VALUES (@Title) SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, platform);

            return data;

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
        public static async Task<int> AddGameAsync(IGameModel game)
        {
            string query = $@"INSERT INTO Game 
                (About, Developer, Publisher, ReleaseDate, SteamDetailsID, Thumbnail, Title)  
                VALUES (@About, @Developer, @Publisher, @ReleaseDate, @SteamDetailsID, @Thumbnail, @Title) 
                SELECT SCOPE_IDENTITY();";

            var data = await SqlDataAccess.SaveDataAsync(query, game);

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
