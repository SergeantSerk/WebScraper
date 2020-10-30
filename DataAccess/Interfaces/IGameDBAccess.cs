using DataAccessLibrary.Models.DatabaseModels;
using DataAccessLibrary.Models.DatabasePostModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Interfaces
{
    public interface IGameDBAccess
    {
        Task<int> AddGameAsync(GameAddModel gameAddModel);
        Task<IEnumerable<GameModel>> GetAllGamesAsync();
        Task<GameModel> GetGameByIdAsync(int id);
        Task<GameModel> GetGameByTitleAsync(string title);
    }
}