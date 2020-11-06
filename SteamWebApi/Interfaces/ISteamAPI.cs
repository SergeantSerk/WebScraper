using Steam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Steam.Interfaces
{
    public interface ISteamAPI
    {
        Task<SteamAppDetails> GetAppBySteamID(int steamID);
        Task<List<SteamApp>> GetApps();
        Task<SteamAppDetails> GetAppBySteamIDProxies(int steamID);
    }
}