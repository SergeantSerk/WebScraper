
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseGetModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using SharedModelLibrary.Models.DatabaseUpdateModels;

namespace DataAccessLibrary.Interfaces
{
    public interface IGameDBAccess
    {
        Task<int> AddGameAsync(GameAddModel gameAddModel);
        Task<IEnumerable<GameModel>> GetAllGamesAsync();
        Task<GameModel> GetGameByIdAsync(int id);
        Task<GameModel> GetGameByTitleAsync(string title);
        Task<int> AddFullGameAsync(FullGameAddModel gameAddModel);
        void AddReleaseDateToGameAsync(ReleaseDateToGameModel rdtg);
        void AddSteamAppToGameAsync(SteamAppToGameModel steamAppToGameModel);
        Task<int> AddSystemRequirementAsync(SystemRequirementAddModel systemRequirement);
        Task<SystemRequirementModel> GetSystemRequirementByGameIdAndPlatformIdAsync(int gameId, int platformId);
        Task<int> AddPlatformAsync(PlatformAddModel platform);
        Task<PlatformModel> GetPlatformByNameAsync(string name);
        Task<PublisherModel> GetPublisherByNameAsync(string name);
        Task<GamePublisherModel> GetGamePublisherAsync(int gameId, int publisherId);
        Task<int> AddGamePublisherAsync(GameAddPublisherModel gameAddPublisher);
        Task<int> AddGameDeveloperAsync(GameAddDeveloperModel gameAddDeveloper);
        Task<GameDeveloperModel> GetGameDeveloperAsync(int gameId, int developerId);
        Task<DeveloperModel> GetDeveloperByNameAsync(string name);
        Task<int> AddDeveloperAsync(string name);
        Task<int> AddPublisherAsync(string name);
        Task<int> AddCurrencyAsync(CurrencyAddModel currency);
        Task<CurrencyModel> GetCurrencyByCodeAsync(string code);
        Task<PriceOverviewModel> GetPriceOverviewByIdAsync(int priceOverviewId);
        Task<int> AddPriceOverviewAsync(PriceOverviewAddModel priceOverview);
        Task<GameDealModel> GetGameDealNotExpiredByStoreIdAsync(int gameId, int storeId);
         Task<StoreModel> GetStoreAsync(string name);
        Task<int> AddStoreAsync(StoreAddModel store);
        Task<int> AddGameDealAsync(GameDealAddModel gameDeal);
        Task<int> AddDealDateAsync(DealDateAddModel dealDate);
        Task<int> ExpireGameDealAsync(int dealdateId);
        Task<int> AddScreenshotsAsync(ScreenshotAddModel screenshot);
        Task<VideoModel> GetVideoByTitleAsync(string title, int gameId);

        Task<VideoContentModel> GetVideoContentByIdAsync(string videoContentId);
        Task<int> AddVideoAsync(VideoAddModel video);
        Task<int> AddVideoContentAsync(VideoContentAddModel video);
        Task<int> AddDLCAsync(DLCAddModel dLC);
        Task<int> AddGameDLCAsync(GameDLCAddModel gameDLC);
        Task<DLCModel> GetDLCBySteamAppIdAsync(int steamAppId);
        Task<GameDLCModel> GetGameDLCByGameIdAndDlcIdAsync(int gameid, int dlcId);
        Task<string> GetGameTitleBySteamAppId(int steamAppId);
        Task<string> GetDLCTitleBySteamAppId(int steamAppId);
        Task<IEnumerable<int>> GetAllSteamIdAsync();
    }
}