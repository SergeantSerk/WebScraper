using Dapper;
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



     
  


        //public static async Task<GameModel> GetGameByTitleAsync(string title)
        //{
        //    //  join game with steam detail
        //    string query = $@"SELECT * FROM Game g 
        //                     LEFT JOIN SteamDetails sd ON  g.SteamDetailsID = sd.ID WHERE g.Title =@Title;";

        //    return await GetGameModelData(query, new { Title = title });

        //}


        //    public static async Task<Tag> GetTagByTitleAsync(string title)
        //    {
        //        string query = $@"SELECT * FROM Tag WHERE Title =@Title;";

        //        var tag = await GetData<Tag>(query, new Tag { Title = title });

        //        return tag.FirstOrDefault(); 
        //    }


        //    public static async Task<Tag> GetTagByIDAsync(int id)
        //    {
        //        string query = $@"SELECT * FROM Tag WHERE ID =@ID;";

        //        var tag = await GetData<Tag>(query, new Tag { ID = id });

        //        return tag.FirstOrDefault();
        //    }


        //    public static async Task<SteamDetailsModel> GetSteamdetailsByIDAsync(int id)
        //    {
        //        string query = $@"Select * FROM Steamdetails WHERE ID = @ID;";

        //        var sd = await GetData<SteamDetailsModel>(query, new SteamDetailsModel { ID = id });

        //        return sd.FirstOrDefault(); 
        //    }


        //    public static async Task<SteamDetailsModel> GetSteamdetailsBySteamIDAsync(string steamID)
        //    {
        //        string query = $@"Select * FROM Steamdetails WHERE SteamID = @SteamID;";

        //        var sd = await GetData<SteamDetailsModel>(query, new SteamDetailsModel { SteamID = steamID });

        //        return sd.FirstOrDefault();
        //    }

        //    public static async Task<PlatformModel> GetPlatformByTitleAsync(string title)
        //    {
        //        string query = $@"Select * FROM Platform WHERE title = @Title;";

        //        var sd = await GetData<PlatformModel>(query, new PlatformModel { Title = title });

        //        return sd.FirstOrDefault();
        //    }
        //    public static async Task<PlatformModel> GetPlatformByIDAsync(int id)
        //    {
        //        string query = $@"Select * FROM Platform WHERE ID = @ID;";

        //        var sd = await GetData<PlatformModel>(query, new PlatformModel { ID = id });

        //        return sd.FirstOrDefault();
        //    }

        //    public static async Task<StoreModel> GetStoreByNameAsync(string name)
        //    {
        //        string query = $@"Select * FROM Store WHERE Name = @Name;";

        //        var sd = await GetData<StoreModel>(query, new StoreModel { Name= name });

        //        return sd.FirstOrDefault();
        //    }


        //    public static async Task<StoreModel> GetStoreByIDAsync(int id)
        //    {
        //        string query = $@"Select * FROM Store WHERE ID = @ID;";

        //        var sd = await GetData<StoreModel>(query, new StoreModel { ID= id});

        //        return sd.FirstOrDefault();
        //    }

        //    public  static async Task<GameTagDetailsModel> GetGameTagDetailsByGameIdAndTagIDAsync(int gameID, int tagID)
        //    {
        //        //  join game with steam detail
        //        string query = $@"SELECT * FROM GameTagDetails gtd 
        //                     LEFT JOIN Tag t ON  gtd.TagID = t.ID WHERE gtd.GameID = @GameID AND gtd.TagID = @TagID;";

        //        return await GetGameTagDetialsData(query, new { GameID = gameID, TagID = tagID });
        //    }

        //    public static async Task<List<MediaModel>> GetMediasByGameIdAsync(int gameID)
        //    {
        //        string query = $@"Select * FROM Media WHERE GameID = @GameID;";

        //        return await GetData<MediaModel>(query, new MediaModel { GameId = gameID }); ;
        //    }


        //    public static async Task<List<DealModel>> GetDealByGameIdAsync(int gameID)
        //    {
        //        string query = $@"Select * FROM Deal WHERE GameID = @GameID AND Expired !=1";

        //        return await GetData<DealModel>(query, new DealModel { GameID = gameID }); ;
        //    }

        //    /// <summary>
        //    /// Return Deal that is not expired for a matchin game id and store id and URl
        //    /// </summary>
        //    /// <param name="gameID"></param>
        //    /// <param name="storeID"></param>
        //    /// <returns></returns>
        //    public static async Task<DealModel> GetDealByGameIdAndStoreIDAsync(int gameID, int storeID,  string url)
        //    {
        //        string query = $@"Select * FROM Deal WHERE GameID = @GameID AND 
        //                        StoreID =@StoreID AND URL=@Url AND Expired != 1;";

        //        var d = await GetData<DealModel>(query, new DealModel { GameID = gameID, StoreID = storeID, URL = url });

        //        return d.FirstOrDefault();
        //    }

        //    public static async Task<MediaModel> GetMediasByGameIdAndUrlAsync(int gameID, string url)
        //    {
        //        string query = $@"Select * FROM Media WHERE GameID = @GameID AND Url=@Url;";

        //        var media = await GetData<MediaModel>(query, new MediaModel { GameId = gameID, Url = url });
        //        return media.FirstOrDefault() ;
        //    }


        //    public static async Task<List<SystemRequirement>> GetSystemRequirementByGameIdAsync(int gameID)
        //    {
        //        string query = $@"Select * FROM SystemRequirement WHERE GameID = @GameID";

        //        var sr = await GetData<SystemRequirement>(query, new SystemRequirement { GameID = gameID });
        //        return sr;
        //    }


        //    public static async Task<SystemRequirement> GetSystemRequirementByGameIdAsync(int gameID, bool minimumRequirement, int platformID)
        //    {
        //        string query = $@"Select * FROM SystemRequirement WHERE GameID = 
        //            @GameID AND MinimumSystemRequirement=@MinimumSystemRequirement AND PlatformID= @PlatformID";

        //        var sr = await GetData<SystemRequirement>(query, new SystemRequirement 
        //        { GameID = gameID, MinimumSystemRequirement = minimumRequirement, PlatformID=platformID });


        //        return sr.FirstOrDefault();
        //    }



        //    public static async Task<int> AddMediaAsync(MediaModel media)
        //    {
        //        string query = $@"INSERT INTO Media (GameID, Url)
        //                            VALUES(@GameID, @Url) SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<MediaModel>(query, media) ;
        //    }


        //    public static async Task<int> AddStoreAsync(StoreModel store)
        //    {
        //        string query = $@"INSERT INTO Store (Name,Logo) VALUES (@Name, @Logo) 
        //                        SELECT SCOPE_IDENTITY() ";

        //        return await SaveDataAsync<StoreModel>(query, store);
        //    }


        //    public static async Task<int> AddPlatformAsync(PlatformModel platform)
        //    {
        //        string query = $@"INSERT INTO Platform (Title)  VALUES (@Title) SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<PlatformModel>(query, platform);
        //    }

        //     public static async Task<int> AddTagAsync(Tag tag)
        //    {
        //        string query = $@"INSERT INTO Tag (Title)  VALUES (@Title) SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<Tag>(query, tag);
        //    }

        //    public static async Task<int> AddGameTagDetailsAsync(GameTagDetailsModel gameTag)
        //    {
        //        string query = $@"INSERT INTO GameTagDetails (GameID, TagID) 
        //                            VALUES (@GameID, @TagID) SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<GameTagDetailsModel>(query, gameTag);
        //    }

        //    public static async Task<int> AddSystemRequirementAsync(SystemRequirement sr)
        //    {
        //        string query = $@"
        //            INSERT INTO SystemRequirement 
        //           (GameID,PlatformId, Requirement, Processor, Os, Memory, Storage, MinimumSystemRequirement)  
        //            VALUES (@GameID,@PlatformId, @Requirement, @Processor, @Os, @Memory, @Storage,@MinimumSystemRequirement) 
        //            SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<SystemRequirement>(query, sr);
        //    }
        //    public static async Task<int> AddSteamDetailsAsync(SteamDetailsModel steamDetailsModel)
        //    {
        //        string query = $@" INSERT INTO Steamdetails (SteamID, SteamReview, SteamReviewCount) 
        //                           VALUES (@SteamID, @SteamReview, @SteamReviewCount) SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<SteamDetailsModel>(query, steamDetailsModel);

        //    }


        //    public static async Task<int> AddDealAsync(DealModel deal)
        //    {
        //        string query = $@"  
        //                   INSERT INTO Deal (GameID, StoreID, Price, PreviousPrice, 
        //                              Expired, ExpiringDate, DatePosted, LimitedTimeDeal, Url, IsFree ) 
        //                             VALUES (@GameID, @StoreID, @Price, @PreviousPrice, 
        //                             @Expired,@ExpiringDate,@DatePosted, @LimitedTimeDeal, 
        //                              @Url, @IsFree) SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<DealModel>(query, deal);

        //    }



        //    public static async Task<int> AddGameAsync(GameModel game)
        //    {
        //        string query = $@"  INSERT INTO Game (About, Developer, Publisher, ReleaseDate,  Thumbnail, Title)  
        //            VALUES (@About, @Developer, @Publisher, @ReleaseDate, @Thumbnail, @Title) 
        //            SELECT SCOPE_IDENTITY()";

        //        return await SaveDataAsync<GameModel>(query,game);

        //    }



        //    /// <summary>
        //    /// /
        //    /// </summary>
        //    /// <returns></returns>
        //    public static async Task<List<FullGameModel>> GetAllFullGames()
        //{
        //    string query = $@"SELECT * FROM Game;
        //                     Select * FROM Steamdetails ;
        //                     SELECT * FROM SystemRequirement;
        //                     SELECT * FROM Platform;
        //                     SELECT * FROM GameTagDetails;
        //                     SELECT * FROM Tag;
        //                     SELECT * FROM Store;
        //                     SELECT * FROM Deal;
        //                     SELECT * FROM Media; ";


        //    return await GetFullGameData(query);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static async Task<FullGameModel> GetFullGameByID(int id)
        //{
        //    string query = $@"SELECT * FROM Game WHERE ID =@ID;
        //                     Select * FROM Steamdetails  ;
        //                     SELECT * FROM SystemRequirement WHERE GameID = @ID;
        //                     SELECT * FROM Platform;
        //                     SELECT * FROM GameTagDetails WHERE GameID = @ID;
        //                     SELECT * FROM Tag;
        //                     SELECT * FROM Store;
        //                     SELECT * FROM Deal WHERE GameID = @ID;
        //                     SELECT * FROM Media WHERE GameID = @ID";

        //    var g = await GetFullGameData(query, new { ID = id });

        //    return g.FirstOrDefault();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="title"></param>
        ///// <returns></returns>
        //public static async Task<FullGameModel> GetFullGameByTitle(string title)
        //{
        //    string query = $@"SELECT * FROM Game WHERE Title =@Title;
        //                     Select * FROM Steamdetails ;
        //                     SELECT * FROM SystemRequirement ;
        //                     SELECT * FROM Platform;
        //                     SELECT * FROM GameTagDetails ;
        //                     SELECT * FROM Tag;
        //                     SELECT * FROM Store;
        //                     SELECT * FROM Deal;
        //                     SELECT * FROM Media ;";


        //    var g = await GetFullGameData(query, new { Title = title });

        //    return g.FirstOrDefault();
        //}


        //protected static async Task<List<FullGameModel>> GetFullGameData(string query, Object param = null)
        //{

        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {

        //        using (var lists = await connection.QueryMultipleAsync(query, param))
        //        {
        //            var fullGameModels = lists.Read<FullGameModel>().ToList();
        //            // convert to  Lookup<TKey,TElement> set key for looking up
        //            var steamDetails = lists.Read<SteamDetailsModel>().ToLookup(sd => sd.ID);


        //            var systemRequirements = lists.Read<SystemRequirement>().ToList();

        //            var platforms = lists.Read<PlatformModel>().ToLookup(p => p.ID);

        //            var gameTagDetails = lists.Read<GameTagDetailsModel>().ToList();

        //            var tag = lists.Read<Tag>().ToLookup(t => t.ID);

        //            var stores = lists.Read<StoreModel>().ToLookup(s => s.ID);

        //            var deals = lists.Read<DealModel>().ToList();

        //            var media = lists.Read<MediaModel>().ToLookup(m => m.GameId);

        //            deals.ForEach(d => d.Store = stores[d.StoreID].FirstOrDefault());
        //            gameTagDetails.ForEach(gtd => gtd.Tag = tag[gtd.TagID].FirstOrDefault());


        //            // set up the platform
        //            systemRequirements.ForEach(sr => sr.Platform = platforms[sr.PlatformID].FirstOrDefault());

        //            var gtdLookup = gameTagDetails.ToLookup(gtd => gtd.GameID);
        //            var srLookUp = systemRequirements.ToLookup(sr => sr.GameID);
        //            var dealLookup = deals.ToLookup(d => d.GameID);

        //            foreach (var game in fullGameModels)
        //            {
        //                game.SystemRequirements = srLookUp[game.ID].ToList();
        //                game.SteamDetail = steamDetails[game.SteamDetailsID].FirstOrDefault();
        //                game.GameTagDetails = gtdLookup[game.ID].ToList();
        //                game.Deals = dealLookup[game.ID].ToList();
        //                game.Medias = media[game.ID].ToList();
        //            }


        //            return fullGameModels;
        //        }

        //    }
        //}



        //protected static async Task<int> SaveDataAsync<T>(string query, T data)
        //{


        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        connection.Open();
        //        using (var trans = connection.BeginTransaction())
        //        {
        //            int index;

        //            try
        //            {
        //                index = await connection.ExecuteScalarAsync<int>(query, data, trans);

                        
        //                trans.Commit();
        //            }
        //            catch(Exception e)
        //            {
        //                Console.WriteLine($"{e}");
        //                trans.Rollback();
        //                index = 0;
        //            }

        //            return index;
        //        }


        //    }
        //}


        //protected static int DeleteData<T>(string query, T data)
        //{
        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {

        //        return connection.Execute(query, data);
        //    }
        //}




        //protected static async Task<List<T>> GetData<T>(string query, T param)
        //{
        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        var game = await connection.QueryAsync<T>(query, param);

        //        return game.ToList();
        //    }
        //}


        //protected static async Task<FullGameModel> GetGameModelData(string query, object param)
        //{
        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        var game = await connection.QueryAsync<FullGameModel, SteamDetailsModel, FullGameModel>
        //            (query, (g, sd) => { g.SteamDetail = sd; return g; }, param);

        //        return game.FirstOrDefault();
        //    }

        //}

        //protected static async Task<GameTagDetailsModel> GetGameTagDetialsData(string query, object param)
        //{
            
        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        connection.Open();

        //        var gameTag = await connection.QueryAsync<GameTagDetailsModel, Tag, GameTagDetailsModel>
        //            (query, (g, t) => { g.Tag = t; return g; }, param);

        //        return gameTag.FirstOrDefault();
        //    }

        //}


    }
}
