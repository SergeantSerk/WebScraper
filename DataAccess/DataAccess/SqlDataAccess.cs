using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class SqlDataAccess: DBAccess
    { 


        public static async Task<GameModel> GetGameByIdAsync(int id)
    {
        //  join game with steam detail
        string query = $@"SELECT * FROM Game g 
                         LEFT JOIN SteamDetails sd ON  g.SteamDetailsID = sd.ID WHERE g.ID = @ID;";

        return await GetGameModelData(query, new { ID = id });

    }

    public static async Task<GameModel> GetGameByTitleAsync(string title)
    {
        //  join game with steam detail
        string query = $@"SELECT * FROM Game WHERE Title =@Title;";

        return await GetGameModelData(query, new { Title = title });

    }

    
        public static async Task<Tag> GetTagByTitleAsync(string title)
        {
            string query = $@"SELECT * FROM Tag WHERE Title =@Title;";

            var tag = await GetData<Tag>(query, new Tag { Title = title });

            return tag.FirstOrDefault(); 
        }


        public static async Task<Tag> GetTagByIDAsync(int id)
        {
            string query = $@"SELECT * FROM Tag WHERE ID =@ID;";

            var tag = await GetData<Tag>(query, new Tag { ID = id });

            return tag.FirstOrDefault();
        }


        public static async Task<SteamDetailsModel> GetSteamdetailsByIDAsync(int id)
        {
            string query = $@"Select * FROM Steamdetails WHERE ID = @ID;";

            var sd = await GetData<SteamDetailsModel>(query, new SteamDetailsModel { ID = id });

            return sd.FirstOrDefault(); 
        }


        public static async Task<SteamDetailsModel> GetSteamdetailsBySteamIDAsync(string steamID)
        {
            string query = $@"Select * FROM Steamdetails WHERE SteamID = @SteamID;";

            var sd = await GetData<SteamDetailsModel>(query, new SteamDetailsModel { SteamID = steamID });

            return sd.FirstOrDefault();
        }

        public static async Task<Platform> GetPlatformByTitleAsync(string title)
        {
            string query = $@"Select * FROM Platform WHERE title = @Title;";

            var sd = await GetData<Platform>(query, new Platform { Title = title });

            return sd.FirstOrDefault();
        }
        public static async Task<Platform> GetPlatformByIDAsync(int id)
        {
            string query = $@"Select * FROM Platform WHERE ID = @ID;";

            var sd = await GetData<Platform>(query, new Platform { ID = id });

            return sd.FirstOrDefault();
        }

        public static async Task<StoreModel> GetStoreByNameAsync(string name)
        {
            string query = $@"Select * FROM Store WHERE Name = @Name;";

            var sd = await GetData<StoreModel>(query, new StoreModel { Name= name });

            return sd.FirstOrDefault();
        }


        public static async Task<StoreModel> GetStoreByIDAsync(int id)
        {
            string query = $@"Select * FROM Store WHERE ID = @ID;";

            var sd = await GetData<StoreModel>(query, new StoreModel { ID= id});

            return sd.FirstOrDefault();
        }

        public  static async Task<GameTagDetailsModel> GetGameTagDetailsByGameIdAndTagIDAsync(int gameID, int tagID)
        {
            //  join game with steam detail
            string query = $@"SELECT * FROM GameTagDetails gtd 
                         LEFT JOIN Tag t ON  gtd.TagID = t.ID WHERE gtd.GameID = @GameID AND gtd.TagID = @TagID;";

            return await GetGameTagDetialsData(query, new { GameID = gameID, TagID = tagID });
        }

        public static async Task<List<MediaModel>> GetMediasByGameIdAsync(int gameID)
        {
            string query = $@"Select * FROM Media WHERE GameID = @GameID;";

            return await GetData<MediaModel>(query, new MediaModel { GameId = gameID }); ;
        }


        public static async Task<List<DealModel>> GetDealByGameIdAsync(int gameID)
        {
            string query = $@"Select * FROM Deal WHERE GameID = @GameID AND Expired !=1";

            return await GetData<DealModel>(query, new DealModel { GameID = gameID }); ;
        }

        /// <summary>
        /// Return Deal that is not expired for a matchin game id and store id and URl
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public static async Task<DealModel> GetDealByGameIdAndStoreIDAsync(int gameID, int storeID,  string url)
        {
            string query = $@"Select * FROM Deal WHERE GameID = @GameID AND 
                            StoreID =@StoreID AND URL=@Url AND Expired != 1;";

            var d = await GetData<DealModel>(query, new DealModel { GameID = gameID, StoreID = storeID, URL = url });

            return d.FirstOrDefault();
        }

        public static async Task<MediaModel> GetMediasByGameIdAndUrlAsync(int gameID, string url)
        {
            string query = $@"Select * FROM Media WHERE GameID = @GameID AND Url=@Url;";

            var media = await GetData<MediaModel>(query, new MediaModel { GameId = gameID, Url = url });
            return media.FirstOrDefault() ;
        }


        public static async Task<List<SystemRequirement>> GetSystemRequirementByGameIdAsync(int gameID)
        {
            string query = $@"Select * FROM SystemRequirement WHERE GameID = @GameID";

            var sr = await GetData<SystemRequirement>(query, new SystemRequirement { GameID = gameID });
            return sr;
        }


        public static async Task<SystemRequirement> GetSystemRequirementByGameIdAsync(int gameID, bool minimumRequirement, int platformID)
        {
            string query = $@"Select * FROM SystemRequirement WHERE GameID = 
                @GameID AND MinimumSystemRequirement=@MinimumSystemRequirement AND PlatformID= @PlatformID";

            var sr = await GetData<SystemRequirement>(query, new SystemRequirement 
            { GameID = gameID, MinimumSystemRequirement = minimumRequirement, PlatformID=platformID });


            return sr.FirstOrDefault();
        }



        public static async Task<int> AddMediaAsync(MediaModel media)
        {
            string query = $@"INSERT INTO Media (GameID, Url)
                                VALUES(@GameID, @Url) SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<MediaModel>(query, media) ;
        }


        public static async Task<int> AddStoreAsync(StoreModel store)
        {
            string query = $@"INSERT INTO Store (Name,Logo) VALUES (@Name, @Logo) 
                            SELECT SCOPE_IDENTITY() ";

            return await SaveDataAsync<StoreModel>(query, store);
        }


        public static async Task<int> AddPlatformAsync(Platform platform)
        {
            string query = $@"INSERT INTO Platform (Title)  VALUES (@Title) SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<Platform>(query, platform);
        }

         public static async Task<int> AddTagAsync(Tag tag)
        {
            string query = $@"INSERT INTO Tag (Title)  VALUES (@Title) SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<Tag>(query, tag);
        }

        public static async Task<int> AddGameTagDetailsAsync(GameTagDetailsModel gameTag)
        {
            string query = $@"INSERT INTO GameTagDetails (GameID, TagID) 
                                VALUES (@GameID, @TagID) SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<GameTagDetailsModel>(query, gameTag);
        }

        public static async Task<int> AddSystemRequirementAsync(SystemRequirement sr)
        {
            string query = $@"
                INSERT INTO SystemRequirement 
               (GameID,PlatformId, Requirement, Processor, Os, Memory, Storage, MinimumSystemRequirement)  
                VALUES (@GameID,@PlatformId, @Requirement, @Processor, @Os, @Memory, @Storage,@MinimumSystemRequirement) 
                SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<SystemRequirement>(query, sr);
        }
        public static async Task<int> AddSteamDetailsAsync(SteamDetailsModel steamDetailsModel)
        {
            string query = $@" INSERT INTO Steamdetails (SteamID, SteamReview, SteamReviewCount) 
                               VALUES (@SteamID, @SteamReview, @SteamReviewCount) SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<SteamDetailsModel>(query, steamDetailsModel);

        }


        public static async Task<int> AddDealAsync(DealModel deal)
        {
            string query = $@"  
                       INSERT INTO Deal (GameID, StoreID, Price, PreviousPrice, 
                                  Expired, ExpiringDate, DatePosted, LimitedTimeDeal, Url, IsFree ) 
                                 VALUES (@GameID, @StoreID, @Price, @PreviousPrice, 
                                 @Expired,@ExpiringDate,@DatePosted, @LimitedTimeDeal, 
                                  @Url, @IsFree) SELECT SCOPE_IDENTITY()";

            return await SaveDataAsync<DealModel>(query, deal);

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
                         SELECT * FROM Media; ";


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
