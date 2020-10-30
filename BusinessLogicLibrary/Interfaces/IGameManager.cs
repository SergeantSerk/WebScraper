using DataAccessLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLibrary.Interfaces
{
    public interface IGameManager
    {
        Task<IEnumerable<GameModel>> GetAllGamesAsync();
        Task<GameModel> GetGameByIdAsync(int id);
    }
}