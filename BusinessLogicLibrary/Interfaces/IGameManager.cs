using DataAccessLibrary.Models.DatabaseModels;
using DataAccessLibrary.Models.DatabasePostModels;
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
    }
}