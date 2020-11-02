using Dapper;
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


        public async Task<GameModel> GetGameByIdAsync(int id)
        {

            string query = $@"SELECT * FROM game g WHERE g.GameId=@GameId";

            return await GetSingleDataAsync<GameModel>(query, new { GameId = id });

        }

        public async Task<GameModel> GetGameByTitleAsync(string title)
        {

            string query = $@"SELECT * FROM game g WHERE g.title=@Title";

            return await GetSingleDataAsync<GameModel>(query, new { Title = title });


        }


        public async Task<int> AddGameAsync(GameAddModel gameAddModel)
        {

            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background,SteamAppId, ReleaseDateId) OUTPUT Inserted.GameId VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background)";

            return await SaveDataAsync(query, gameAddModel);


        }

        public async Task<int> AddFullGameAsync(FullGameAddModel gameAddModel)
        {

            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background,SteamAppId, ReleaseDateId) OUTPUT Inserted.GameId VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background,@SteamAppId, @ReleaseDateId)";

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
                                WHERE dd.Expired != 'true' AND GameId=@GameId AND StoreId=@StoreId";

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


        public async void AddSteamAppToGameAsync(SteamAppToGameModel steamAppToGameModel)
        {
            string query = $@"UPDATE game SET SteamAppId=@SteamAppId WHERE game.GameId =@GameId";

            SaveDataAsync(query, steamAppToGameModel);
        }
    }
}
