using Dapper;
using DataAccessLibrary.DataAccess.DBAccessFactory;
using DataAccessLibrary.Models.DatabaseModels;
using DataAccessLibrary.Models.DatabasePostModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public static class GameDBAccess
    {
        public static async Task<IEnumerable<GameModel>> GetAllGamesAsync()
        {
            var query = $"SELECT * FROM game";

            return await DBFactory.GetAllDataAsync<GameModel>(query);
        }


        public static async Task<GameModel> GetGameByIdAsync(int id)
        {
          
            string query = $@"SELECT * FROM game g WHERE g.game_id=@GameId";

            return await DBFactory.GetSingleDataAsync<GameModel>(query, new { GameId = id });

           
        }

        public static async Task<GameModel> GetGameByTitleAsync(string title)
        {

            string query = $@"SELECT * FROM game g WHERE g.title=@Title";

            return await DBFactory.GetSingleDataAsync<GameModel>(query, new { Title = title });


        }


        public static async Task<int> AddGameAsync(GameAddModel gameAddModel)
        {

            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background) VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background)";

            return await DBFactory.SaveDataAsync(query, gameAddModel);


        }


    }
}
