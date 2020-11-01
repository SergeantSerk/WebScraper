using Dapper;
using DataAccessLibrary.DataAccess.Abstraction;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseGetModels;
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

        public async Task<int> AddSystemRequirementAsync(SystemRequirementAddModel systemRequirement)
        {

            string query = $@"INSERT INTO SystemRequirement (GameId, PlatformId, Minimum, Recommended)
                    OUTPUT Inserted.SystemRequirementId VALUES(@GameId, @PlatformId, @Minimum, @Recommended)";

            return await SaveDataAsync(query, systemRequirement);


        }

        public async Task<SystemRequirementModel> GetSystemRequirementByGameIdAndPlatformIdAsync(int gameId, int platformId)
        {

            string query = $@"SELECT * FROM SystemRequirement WHERE GameId=@GameId AND PlatformId=@Platformid";

            return await GetSingleDataAsync<SystemRequirementModel>(query, new { GameId = gameId, PlatformId = platformId });

        }

        public async Task<int> AddPlatformAsync(PlatformAddModel platform)
        {

            string query = $@"INSERT INTO Platform (Name)
                    OUTPUT Inserted.PlatformId VALUES(@Name)";

            return await SaveDataAsync(query, platform);
        }

        public async Task<PlatformModel> GetPlatformByNameAsync(string name)
        {

            string query = $@"SELECT * FROM Platform
                    WHERE Name=@Name";

            return await GetSingleDataAsync<PlatformModel>(query, new { Name=name});


        }


        public async Task<PublisherModel> GetPublisherByNameAsync(string name)
        {

            string query = $@"SELECT * FROM Publisher
                    WHERE Name=@Name";

            return await GetSingleDataAsync<PublisherModel>(query, new { Name = name });


        }

        public async Task<GamePublisherModel> GetGamePublisherAsync( int gameId, int publisherId)
        {

            string query = $@"SELECT * FROM GamePublisher
                    WHERE PublisherId=@PublisherId AND GameId=@GameId";

            return await GetSingleDataAsync<GamePublisherModel>(query, new { PublisherId = publisherId, GameId=gameId });


        }

        public async Task<int> AddGamePublisherAsync(GameAddPublisherModel gameAddPublisher)
        {

            string query = $@"INSERT INTO GamePublisher (GameId, PublisherId)
                    VALUES(@GameId, @PublisherId)";

            return await SaveDataAsync(query, gameAddPublisher);

        }
        public async Task<int> AddGameDeveloperAsync(GameAddDeveloperModel gameAddDeveloper)
        {

            string query = $@"INSERT INTO GameDeveloper (GameId, DeveloperId)
                    VALUES(@GameId, @DeveloperId)";

            return await SaveDataAsync(query, gameAddDeveloper);


        }

        public async Task<GameDeveloperModel> GetGameDeveloperAsync(int gameId, int developerId)
        {

            string query = $@"SELECT * FROM GameDeveloper
                    WHERE DeveloperId=@DeveloperId AND GameId=@GameId";

            return await GetSingleDataAsync<GameDeveloperModel>(query, new { DeveloperId = developerId, GameId = gameId });


        }


        public async Task<DeveloperModel> GetDeveloperByNameAsync(string name)
        {

            string query = $@"SELECT * FROM Developer
                    WHERE Name=@Name";

            return await GetSingleDataAsync<DeveloperModel>(query, new { Name = name });


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
