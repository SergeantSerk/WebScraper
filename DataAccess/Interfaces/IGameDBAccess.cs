
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModelLibrary.Models.DatabaseAddModels;
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
    }
}