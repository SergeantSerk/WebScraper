
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



    }
}