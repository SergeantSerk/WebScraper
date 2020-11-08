
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLibrary.Interfaces
{
    public interface IGameManager
    {
        Task<IEnumerable<GameModel>> GetAllGamesAsync();
        Task<GameModel> GetGameByIdAsync(int id);

        Task<GameModel> GetGameByTitleAsync(string title);

        Task<int> AddGameAsync(GameAddModel game);

        Task<int> AddSteamApp(SteamAppAddModel steamApp);

        Task<int> AddReleaseDate(ReleaseDateAddModel releaseDate);
        Task<int> AddFullGameAsync(FullGameAddModel game);
        Task ValidateReleaseDate(int? releaseDateID, ReleaseDateAddModel releaseDate);
        Task<int> AddCategory(string description);

        Task<int> AddGenre(string description);
        Task AddGenreToGameByDescription(int gameId, string genreDescription);
        Task AddCategoryToGameByDescription(int gameId, string categoryDescription);
        Task<int> AddSystemRequirement(SystemRequirementAddModel systemRequirement);
        Task<int> AddPlatform(PlatformAddModel platform);
        Task<int> AddGameDeveloperAsync(int gameId, string developer);
        Task<int> AddGamePublisherAsync(int gameId, string publisher);
        Task<int> AddPublisher(string name);
        Task<int> AddDeveloper(string name);

        Task<int> AddStore(StoreAddModel store);

        Task<int> AddDealDate(DealDateAddModel deal);
        Task<int> AddGameDeal(GameDealAddModel gameDeal);

        Task<int> AddPriceOverview(PriceOverviewAddModel priceOverview);
        Task AddVideoAsync(VideoAddModel video);
        Task AddGameDLC(GameDLCAddModel gameDLC);
        Task<int> AddDLC(DLCAddModel dLC);

        Task<List<int>> GetAllSteamIdAsync();

            }
}