using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using SharedModelLibrary.Models.DatabaseUpdateModels;
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

            string query = $@"SELECT * FROM game g WHERE g.GameId=@GameId";

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
                   HeaderImage, Background,SteamAppId, ReleaseDateId) OUTPUT Inserted.GameId VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background)";

            return await SaveDataAsync(query, gameAddModel);


        }

        public async Task<int> AddFullGameAsync(FullGameAddModel gameAddModel)
        {

            string query = $@"INSERT INTO game (Title, Type, About, Website, Thumbnail, Description,
                   HeaderImage, Background,SteamAppId, ReleaseDateId) OUTPUT Inserted.GameId VALUES(@Title, @Type,@About, @Website,@Thumbnail, @Description,
                   @HeaderImage, @Background,@SteamAppId, @ReleaseDateId)";

            return await SaveDataAsync(query, gameAddModel);


        }

        public async void AddReleaseDateToGameAsync(ReleaseDateToGameModel rdtg)
        {

            string query = $@"UPDATE game SET ReleaseDateId=@ReleaseDateId WHERE game.GameId =@GameId";

            SaveDataAsync(query, rdtg);
        }


        public async void AddSteamAppToGameAsync(SteamAppToGameModel steamAppToGameModel)
        {
            string query = $@"UPDATE game SET SteamAppId=@SteamAppId WHERE game.GameId =@GameId";

            SaveDataAsync(query, steamAppToGameModel);
        }
    }
}
