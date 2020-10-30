using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class GameDBAccess : DBAccessAbstraction, IGameDBAccess
    {
        public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
        {
            var query = $"SELECT * FROM game";

            return await GetAllDataAsync<GameModel>(query);
        }


        public async Task<GameModel> GetGameByIdAsync(int id)
        {

            string query = $@"SELECT * FROM game g WHERE g.game_id=@GameId";

            return await GetSingleDataAsync<GameModel>(query, new { GameId = id });

        }

        public async Task<GameModel> GetGameByTitleAsync(string title)
        {

            string query = $@"SELECT * FROM game g WHERE g.title=@Title";

            return await GetSingleDataAsync<GameModel>(query, new { Title = title });


        }


        public async Task<int> AddGameAsync(GameAddModel gameAddModel)
        {

            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background) OUTPUT Inserted.GameId VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background)";

            return await SaveDataAsync(query, gameAddModel);


        }


    }
}
