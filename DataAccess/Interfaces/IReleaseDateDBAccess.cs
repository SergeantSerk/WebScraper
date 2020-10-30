using SharedModelLibrary.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Interfaces
{
    public interface IReleaseDateDBAccess
    {
        Task<IEnumerable<ReleaseDateModel>> GetAllReleaseDateAsync();
        Task<ReleaseDateModel> GetReleaseDateByIdAsync(int id);
    }
}