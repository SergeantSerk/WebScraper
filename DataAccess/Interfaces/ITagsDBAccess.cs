
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public interface ITagsDBAccess
    {
        Task<int> AddCategoryAsync(string description);
        Task<int> AddGenreAsync(string genreDescription);
        Task<CategoryModel> GetCategoryByDescriptionAsync(string description);
        Task<GenreModel> GetGenreByDescriptionAsync(string description);
        void AddGenreToGame(GameGenreModel gameAddGenreModel);
        void AddCategoryToGame(GameCategoryModel gameAddCategoryModel);
        Task<GameCategoryModel> GetGameCategory(GameCategoryModel gameCategory);
        Task<GameGenreModel> GetGameGenre(GameGenreModel gameGenre);
    }
}