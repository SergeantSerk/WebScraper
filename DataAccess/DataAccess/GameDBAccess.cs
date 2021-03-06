﻿using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseGetModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using SharedModelLibrary.Models.DatabaseUpdateModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class GameDBAccess : DBAccessAbstraction, IGameDBAccess
    {
        public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
        {
            var query = $"SELECT * FROM game";

            return await GetAllDataAsync<GameModel>(query);
        }

        public async Task<IEnumerable<int>> GetAllSteamIdAsync()
        {
            var query = "SELECT SteamAppId FROM SteamApp";


            return await GetAllDataAsync<int>(query);
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
        {

            string query = $@"SELECT * FROM game g WHERE g.GameId=@GameId";

            return await GetSingleDataAsync<GameModel>(query, new { GameId = id });

        }

        public async Task<string> GetGameTitleBySteamAppId(int steamAppId)
        {
            string query = $@"SELECT Title FROM game  WHERE SteamAppId=@SteamAppId";

            return await GetSingleDataAsync<string>(query, new { SteamAppId = steamAppId });
        }

        public async Task<string> GetDLCTitleBySteamAppId(int steamAppId)
        {
            string query = $@"SELECT Title FROM game  WHERE SteamAppId=@SteamAppId AND Type ='dlc'";

            return await GetSingleDataAsync<string>(query, new { SteamAppId = steamAppId });
        }


        public async Task<GameModel> GetGameByTitleAsync(string title)
        {

            string query = $@"SELECT * FROM game g WHERE g.title=@Title";

            return await GetSingleDataAsync<GameModel>(query, new { Title = title });


        }


        public async Task<int> AddGameAsync(GameAddModel gameAddModel)
        {
            // need to fix
            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background,SteamAppId, ReleaseDateId) OUTPUT Inserted.GameId 
    VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background)";

            return await SaveDataAsync(query, gameAddModel);


        }

        public async Task<int> AddFullGameAsync(FullGameAddModel gameAddModel)
        {

            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background,SteamAppId, ReleaseDateId, ShortDescription) OUTPUT Inserted.GameId 
            VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background,@SteamAppId, @ReleaseDateId, @ShortDescription)";

            return await SaveDataAsync(query, gameAddModel);


        }

        public async Task<int> AddSystemRequirementAsync(SystemRequirementAddModel systemRequirement)
        {

            string query = $@"INSERT INTO SystemRequirement (GameId, PlatformId, Minimum, Recommended)
                    OUTPUT Inserted.SystemRequirementId VALUES(@GameId, @PlatformId, @Minimum, @Recommended)";

            return await SaveDataAsync(query, systemRequirement);


        }

        public async Task<SystemRequirementModel> GetSystemRequirementByGameIdAndPlatformIdAsync(int gameId, int platformId)
        {

            string query = $@"SELECT * FROM SystemRequirement WHERE GameId=@GameId AND PlatformId=@Platformid";

            return await GetSingleDataAsync<SystemRequirementModel>(query, new { GameId = gameId, PlatformId = platformId });

        }

        public async Task<int> AddPlatformAsync(PlatformAddModel platform)
        {

            string query = $@"INSERT INTO Platform (Name)
                    OUTPUT Inserted.PlatformId VALUES(@Name)";

            return await SaveDataAsync(query, platform);
        }

        public async Task<PlatformModel> GetPlatformByNameAsync(string name)
        {

            string query = $@"SELECT * FROM Platform
                    WHERE Name=@Name";

            return await GetSingleDataAsync<PlatformModel>(query, new { Name=name});


        }


        public async Task<int> AddPublisherAsync(string name)
        {

            string query = $@"INSERT INTO Publisher (Name) OUTPUT INSERTED.PublisherId
                    VALUES(@Name)";
            return await SaveDataAsync(query, new { Name = name });

        }

        public async Task<int> AddDeveloperAsync(string name)
        {

            string query = $@"INSERT INTO Developer (Name) OUTPUT INSERTED.DeveloperId
                    VALUES(@Name)";
            return await SaveDataAsync(query, new { Name = name });

        }


            public async Task<PublisherModel> GetPublisherByNameAsync(string name)
        {

            string query = $@"SELECT * FROM Publisher
                    WHERE Name=@Name";

            return await GetSingleDataAsync<PublisherModel>(query, new { Name = name });


        }

        public async Task<GamePublisherModel> GetGamePublisherAsync( int gameId, int publisherId)
        {

            string query = $@"SELECT * FROM GamePublisher
                    WHERE PublisherId=@PublisherId AND GameId=@GameId";

            return await GetSingleDataAsync<GamePublisherModel>(query, new { PublisherId = publisherId, GameId=gameId });


        }

        public async Task<int> AddGamePublisherAsync(GameAddPublisherModel gameAddPublisher)
        {

            string query = $@"INSERT INTO GamePublisher (GameId, PublisherId)
                    VALUES(@GameId, @PublisherId)";

            return await SaveDataAsync(query, gameAddPublisher);

        }
        public async Task<int> AddGameDeveloperAsync(GameAddDeveloperModel gameAddDeveloper)
        {

            string query = $@"INSERT INTO GameDeveloper (GameId, DeveloperId)
                    VALUES(@GameId, @DeveloperId)";

            return await SaveDataAsync(query, gameAddDeveloper);


        }

        public async Task<GameDeveloperModel> GetGameDeveloperAsync(int gameId, int developerId)
        {

            string query = $@"SELECT * FROM GameDeveloper
                    WHERE DeveloperId=@DeveloperId AND GameId=@GameId";

            return await GetSingleDataAsync<GameDeveloperModel>(query, new { DeveloperId = developerId, GameId = gameId });


        }


        public async Task<DeveloperModel> GetDeveloperByNameAsync(string name)
        {

            string query = $@"SELECT * FROM Developer
                    WHERE Name=@Name";

            return await GetSingleDataAsync<DeveloperModel>(query, new { Name = name });
        }


        public async Task<CurrencyModel> GetCurrencyByCodeAsync(string code)
        {

            string query = $@"SELECT * FROM Currency
                    WHERE Code=@Code";

            return await GetSingleDataAsync<CurrencyModel>(query, new { Code = code });
        }



        public async Task<int> AddCurrencyAsync(CurrencyAddModel currency)
        {

            string query = $@"INSERT INTO Currency (Code,Symbole) OUTPUT INSERTED.CurrencyId
                            VALUES(@Code,@Symbole)";

            return await GetSingleDataAsync<int>(query, currency);
        }


        public async Task<PriceOverviewModel> GetPriceOverviewByIdAsync(int priceOverviewId)
        {

            string query = $@"SELECT * FROM PriceOverview WHERE PriceOverviewId=@PriceOverviewId";

            return await GetSingleDataAsync<PriceOverviewModel>(query, new { PriceOverviewId = priceOverviewId });
        }



        public async Task<int> AddPriceOverviewAsync(PriceOverviewAddModel priceOverview)
        {

            string query = $@"INSERT INTO PriceOverview 
                        (Price, PriceFormat, FinalPrice, FinalPriceFormat, CurrencyId, DiscountPercentage)
                    OUTPUT INSERTED.PriceOverviewId
                        VALUES(@Price, @PriceFormat, @FinalPrice, @FinalPriceFormat, @CurrencyId, @DiscountPercentage)
                    ";

            return await SaveDataAsync(query, priceOverview);
        }


        public async Task<GameDealModel> GetGameDealNotExpiredByStoreIdAsync(int gameId, int storeId)
        {

            string query = $@"SELECT * FROM GameDeal gd LEFT JOIN DealDate dd ON  dd.DealDateId =gd.DealDateId
                                WHERE dd.Expired != 'True' AND GameId=@GameId AND StoreId=@StoreId";

            return await GetSingleDataAsync<GameDealModel>(query, new { GameId=gameId, StoreId=storeId });
        }

        public async Task<int> AddGameDealAsync(GameDealAddModel gameDeal)
        {

            string query = $@"INSERT INTO  GameDeal (Url, StoreId, GameId, PriceOverviewId,DealDateId,IsFree)
                        VALUES(@Url, @StoreId, @GameId, @PriceOverviewId,@DealDateId,@IsFree)";

            return await SaveDataAsync(query, gameDeal); 
        }


        public async Task<int> AddDealDateAsync(DealDateAddModel dealDate)
        {

            string query = $@"INSERT INTO  DealDate (DatePosted, ExpiringDate, LimitedTimeDeal, Expired)
                           OUTPUT INSERTED.DealDateId
                        VALUES(@DatePosted, @ExpiringDate, @LimitedTimeDeal, @Expired)";

            return await SaveDataAsync(query, dealDate);
        }


        public async Task<int> AddScreenshotsAsync(ScreenshotAddModel screenshot)
        {

            string query = $@"INSERT INTO  Screenshot (GameId, PathFull,PathThumbnail)
                           OUTPUT INSERTED.ScreenshotId
                        VALUES(@GameId, @PathFull,@PathThumbnail)";

            return await SaveDataAsync(query, screenshot);
        }

        public async Task<int> ExpireGameDealAsync(int  dealdateId)
        {

            string query = $@"UPDATE DealDate SET Expired ='True' WHERE DealDateId=@DealDateId";

            return await SaveDataAsync(query, new {DealDateId=dealdateId });
        }


        public async Task<VideoModel> GetVideoByTitleAsync(string title, int gameId)
        {

            string query = $@"SELECT * FROM Video WHERE GameId=@GameId AND Title=@Title";


            return await GetSingleDataAsync<VideoModel>(query, new { GameId = gameId, Title = title });

        }

        public async Task<int> AddVideoAsync(VideoAddModel video)
        {
            string query = $@"INSERT INTO Video (Title,Thumbnail, Highlight, WebmId, Mp4Id, GameId)
                            OUTPUT INSERTED.VideoId VALUES (@Title,@Thumbnail, @Highlight, @WebmId, @Mp4Id, @GameId)";

            return await SaveDataAsync(query, video);
        }

        public async Task<int> AddVideoContentAsync(VideoContentAddModel video)
        {
            string query = $@"INSERT INTO VideoContent (Quality,Max, MediaType )
                            OUTPUT INSERTED.VideoContentId 
                            VALUES (@Quality,@Max, @MediaType)";

            return await SaveDataAsync(query, video);
        }

        public async Task<VideoContentModel> GetVideoContentByIdAsync(string videoContentId)
        {

            string query = $@"SELECT * FROM VideoContent WHERE VideoContentId=@VideoContentId";

            return await GetSingleDataAsync<VideoContentModel>(query, new { VideoContentId=videoContentId });
        }


        public async Task<StoreModel> GetStoreAsync(string name)
        {
            string query = $@"SELECT * FROM Store WHERE Name=@name";

            return await GetSingleDataAsync<StoreModel>(query, new { Name = name});
        }

        public async Task<int> AddStoreAsync(StoreAddModel store)
        {
            string query = $@"INSERT INTO Store (Name, Logo) OUTPUT INSERTED.StoreId VALUES (@Name, @Logo)";

            return await SaveDataAsync(query, store);
        }


        public async void AddReleaseDateToGameAsync(ReleaseDateToGameModel rdtg)
        {

            string query = $@"UPDATE game SET ReleaseDateId=@ReleaseDateId WHERE game.GameId =@GameId";

            SaveDataAsync(query, rdtg);
        }

        public async Task<int> AddDLCAsync(DLCAddModel dLC)
        {

            string query = $@"INSERT INTO DLC (SteamAppId, Title) OUTPUT INSERTED.DLCId VALUES (@SteamAppId, @Title)";

            return await SaveDataAsync(query, dLC);
        }

        public async Task<int> AddGameDLCAsync(GameDLCAddModel gameDLC)
        {

            string query = $@"INSERT INTO GameDLC (GameId, DLCId) VALUES (@GameId, @DLCId)";

            return await SaveDataAsync(query, gameDLC);
        }

        public async Task<DLCModel> GetDLCBySteamAppIdAsync(int steamAppId)
        {

            string query = $@"SELECT * FROM  DLC WHERE SteamAppId=@SteamAppId";

            return await GetSingleDataAsync<DLCModel>(query, new { SteamAppId = steamAppId });
        }

        public async Task<GameDLCModel> GetGameDLCByGameIdAndDlcIdAsync(int gameid, int dlcId)
        {

            string query = $@"SELECT * FROM  GameDLC WHERE GameId=@GameId AND DLCId=@DLCId";

            return await GetSingleDataAsync<GameDLCModel>(query, new { GameId=gameid, DLCId=dlcId });
        }


        public async void AddSteamAppToGameAsync(SteamAppToGameModel steamAppToGameModel)
        {
            string query = $@"UPDATE game SET SteamAppId=@SteamAppId WHERE game.GameId =@GameId";

            SaveDataAsync(query, steamAppToGameModel);
        }
    }
}
