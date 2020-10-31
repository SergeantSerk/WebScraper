using DataAccessLibrary.DataAccess.Abstraction;

using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class TagsDBAccess : DBAccessAbstraction, ITagsDBAccess
    {
        public async Task<int> AddGenreAsync(string description)
        {
            string query = @"INSERT INTO Genre (Description) 
                            OUTPUT INSERTED.GenreId
                                   VALUES(@Description)";

            return await SaveDataAsync(query, new { Description = description });
        }

        public async Task<GenreModel> GetGenreByDescriptionAsync(string description)
        {
            string query = @"SELECT * FROM Genre WHERE Description=@Description";

            return await GetSingleDataAsync<GenreModel>(query, new { Description = description });
        }


        public async Task<GameGenreModel> GetGameGenre(GameGenreModel gameGenre )
        {
            string query = @"SELECT * FROM GameGenre WHERE GameId=@GameId AND GenreId=@GenreId";

            return await GetSingleDataAsync<GameGenreModel>(query, gameGenre);
        }
        public async Task<GameCategoryModel> GetGameCategory(GameCategoryModel gameCategory)
        {
            string query = @"SELECT * FROM GameCategory gc WHERE gc.GameId=@GameId AND gc.CategoryId=@CategoryId";

            return await GetSingleDataAsync<GameCategoryModel>(query, gameCategory);
        }

        public async Task<CategoryModel> GetCategoryByDescriptionAsync(string description)
        {
            string query = @"SELECT * FROM Category WHERE Description=@Description";

            return await GetSingleDataAsync<CategoryModel>(query, new { Description = description });
        }

        public async Task<int> AddCategoryAsync(string description)
        {
            string query = @"INSERT INTO Category (Description) 
                            OUTPUT INSERTED.CategoryId
                                   VALUES(@Description)";

            return await SaveDataAsync(query, new { Description = description });
        }

         public async void AddGenreToGame(GameGenreModel gameAddGenreModel)
        {

            string query = $@"INSERT INTO GameGenre (GameId, GenreId) VALUES(@GameId, @GenreId)";

            await SaveDataAsync(query, gameAddGenreModel);
        }

        public async void AddCategoryToGame(GameCategoryModel gameAddCategoryModel)
        {

            string query = $@"INSERT INTO GameCategory (GameId, CategoryId) VALUES(@GameId, @CategoryId)";

            await SaveDataAsync(query, gameAddCategoryModel);
        }
    }
}
