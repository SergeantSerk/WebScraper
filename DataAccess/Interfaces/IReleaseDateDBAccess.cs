using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabaseUpdateModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Interfaces
{
    public interface IReleaseDateDBAccess
    {
        Task<IEnumerable<ReleaseDateModel>> GetAllReleaseDateAsync();
        Task<ReleaseDateModel> GetReleaseDateByIdAsync(int id);
        Task<int> AddReleaseDateAsync(ReleaseDateAddModel releaseDate);
        void UpdateReleaseDateAsync(ReleaseDateUpdateModel releaseDate);
    }
}