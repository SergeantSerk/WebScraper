using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Interfaces
{
    public interface ISteamAppDbAccess
    {
        Task<IEnumerable<SteamAppModel>> GetAllSteamAppsAsync();
        Task<SteamAppModel> GetSteamAppByIdAsync(int id);

        Task<int> AddSteamAppAsync(SteamAppAddModel steamApp);

    }
}