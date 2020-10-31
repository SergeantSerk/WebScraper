using BusinessAccessLibrary.Interfaces;
using BusinessAccessLibrary.Utilities;
using DataAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabaseModels;
using SharedModelLibrary.Models.DatabasePostModels;
using SharedModelLibrary.Models.DatabaseUpdateModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLibrary.BusinessLogic
{
    public class GameManager : IGameManager
    {
        private readonly IGameDBAccess _gamedbAccess;
        private readonly IReleaseDateDBAccess _releaseDateDBAccess;
        private readonly ISteamAppDbAccess _steamAppDbAccess;

        public GameManager(IGameDBAccess gamedbAccess, 
            IReleaseDateDBAccess releaseDateDBAccess, 
            ISteamAppDbAccess steamAppDbAccess)
        {
            _gamedbAccess = gamedbAccess;
            _releaseDateDBAccess = releaseDateDBAccess;
            _steamAppDbAccess = steamAppDbAccess;
        }

        public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
        {
            var games = await _gamedbAccess.GetAllGamesAsync();

            return games;
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            if (id <= 0)
            {

                var game = await _gamedbAccess.GetGameByIdAsync(id);

                return game;
            }

            throw new Exception("Zero or negative number is invalid input");
        }

        public async Task<int> AddGameAsync(GameAddModel game)
        {
            var validator = DataValidatorHelper.Validate(game);


            if(validator.IsValid)
            {
                var gameDB = await GetGameByTitleAsync(game.Title);

                if (gameDB == null)
                {
                    Console.WriteLine($"{game.Title} has been added to database");
                    
                    return await _gamedbAccess.AddGameAsync(game);
                }

                return gameDB.GameID;
            }


            Console.WriteLine($"Invalid Data from {nameof(GameAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");
        }

        public async Task<GameModel> GetGameByTitleAsync(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var game = await _gamedbAccess.GetGameByTitleAsync(title);
                return game;
            }


            throw new ArgumentNullException("Title cannot be null");
        }


        public async Task<int> AddSteamApp(SteamAppAddModel steamApp)

        {
            var validator = DataValidatorHelper.Validate(steamApp);

            if(validator.IsValid)
            {
                var steamAppDB = await _steamAppDbAccess.GetSteamAppByIdAsync(steamApp.SteamAppId);

                if(steamAppDB == null)
                {

                    return await _steamAppDbAccess.AddSteamAppAsync(steamApp);
                }

                return steamAppDB.SteamAppId;

            }
            Console.WriteLine($"Invalid Data from {nameof(SteamAppAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");
        }

        public async Task<int> AddReleaseDate(ReleaseDateAddModel releaseDate)
        {
            var validator = DataValidatorHelper.Validate(releaseDate);

            if(validator.IsValid)
            {
                return await _releaseDateDBAccess.AddReleaseDateAsync(releaseDate);
            }

            Console.WriteLine($"Invalid Data from {nameof(ReleaseDateAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");
        }

        public async Task<int> AddFullGameAsync(FullGameAddModel game)
        {
            var validator = DataValidatorHelper.Validate(game);

            if (validator.IsValid)
            {
                var gameDB = await _gamedbAccess.GetGameByTitleAsync(game.Title);

                if(gameDB == null)
                {

                    var releaseDateID = await AddReleaseDate(game.ReleaseDate);
                    var steamAppID = await AddSteamApp(game.steamApp);

                    game.SteamAppId = steamAppID;
                    game.ReleaseDateId = releaseDateID;

                    return await _gamedbAccess.AddFullGameAsync(game);

                }

                if(gameDB.ReleaseDateID == null || gameDB.ReleaseDateID == 0)
                {

                    var releaseDateID = await AddReleaseDate(game.ReleaseDate);

                    AddReleaseDateToGameAsync(new ReleaseDateToGameModel 
                    { GameId = gameDB.GameID, ReleaseDateId = releaseDateID});
                }

                if(gameDB.SteamAppId == null || gameDB.SteamAppId == 0)
                {

                    var steamAppId = await AddSteamApp(game.steamApp);

                    AddSteamAppToGameAsync(new SteamAppToGameModel 
                    { GameId=gameDB.GameID, SteamAppId = steamAppId});
                }

                return gameDB.GameID;

            }

            Console.WriteLine($"Invalid Data from {nameof(FullGameAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");
        }



        public async void AddSteamAppToGameAsync(SteamAppToGameModel steamAppToGameModel)
        {
            var validator = DataValidatorHelper.Validate(steamAppToGameModel);

            if (validator.IsValid)
            {
                _gamedbAccess.AddSteamAppToGameAsync(steamAppToGameModel);
            }
            else
            {
                Console.WriteLine($"Invalid Data from {nameof(SteamAppToGameModel)}");
                validator.Errors.ForEach(e => Console.WriteLine(e));

                throw new Exception("Some data are invalid");
            }
        }

        public async void AddReleaseDateToGameAsync(ReleaseDateToGameModel releaseDateToGameModel)
        {
            var validator = DataValidatorHelper.Validate(releaseDateToGameModel);

            if (validator.IsValid)
            {
                _gamedbAccess.AddReleaseDateToGameAsync(releaseDateToGameModel);
            }
            else
            {

                Console.WriteLine($"Invalid Data from {nameof(ReleaseDateToGameModel)}");
                validator.Errors.ForEach(e => Console.WriteLine(e));

                throw new Exception("Some data are invalid");
            }
        }




        //public static async Task<int> AddGameAsync(GameModel g)
        //{


        //    if (DataValidatorHelper.IsValid(g))
        //    {
        //        var game = await SqlDataAccess.GetGameByTitleAsync(g.Title);

        //        if (game == null)
        //        {

        //            return await SqlDataAccess.AddGameAsync(g);
        //        }

        //        return game.ID;
        //    }

        //    return 0;
        //}



        //public static async Task<int>  AddMediaAsync(MediaModel media)
        //{


        //    if (DataValidatorHelper.IsValid(media))
        //    {
        //        var game = await SqlDataAccess.GetGameByIdAsync(media.GameId);

        //        if (game != null)
        //        {
        //            var checkMedia = await SqlDataAccess.GetMediasByGameIdAndUrlAsync(game.ID, media.Url);

        //            if (checkMedia != null)
        //            {
        //                return checkMedia.ID;
        //            }

        //            return await SqlDataAccess.AddMediaAsync(media);
        //        }
        //    }

        //    return 0;
        //}



        //public static async Task<int> AddStoreAsync(StoreModel store)
        //{


        //    if(DataValidatorHelper.IsValid(store))
        //    {
        //        var checkStore = await SqlDataAccess.GetStoreByNameAsync(store.Name);

        //        if(checkStore != null)
        //        {

        //            return checkStore.ID;
        //        }

        //        return await SqlDataAccess.AddStoreAsync(store);

        //    }

        //    Console.WriteLine("Store had insufficient data to add to database");

        //    return 0;

        //}


        //public static async Task<int> AddPlatformAsync(PlatformModel platform)
        //{

        //    if(DataValidatorHelper.IsValid(platform))
        //    {
        //        var plf = await SqlDataAccess.GetPlatformByTitleAsync(platform.Title);

        //        if(plf != null)
        //        {
        //            return plf.ID;
        //        }

        //        return await SqlDataAccess.AddPlatformAsync(platform);
        //    }

        //    return 0;

        //}


        //public static async Task<int> AddGameTagDetailsAsync(GameTagDetailsModel gtd)
        //{




        //    if(DataValidatorHelper.IsValid(gtd))
        //    {

        //        var gtdDB = await SqlDataAccess.GetGameTagDetailsByGameIdAndTagIDAsync
        //            (gtd.GameID, gtd.TagID);

        //        if (gtdDB == null)
        //        {

        //            var game = await SqlDataAccess.GetGameByIdAsync(gtd.GameID);
        //            var tag = await SqlDataAccess.GetTagByIDAsync(gtd.TagID);

        //            if (tag != null && game != null)
        //            {

        //                var data = await SqlDataAccess.AddGameTagDetailsAsync(gtd);

        //                return data;

        //            }
        //            return gtdDB.ID;
        //        }



        //    }


        //    return 0;
        //}






        //public static async Task<int> AddTagAsync(Tag tag)
        //{
        //    if (DataValidatorHelper.IsValid(tag))
        //    {
        //        var tagDB = await SqlDataAccess.GetTagByTitleAsync(tag.Title);

        //        if (tagDB == null)
        //        {

        //            return await SqlDataAccess.AddTagAsync(tag);
        //        }

        //        return tagDB.ID;
        //    }

        //    return 0;

        //}





        //public static async Task<int> AddSystemRequirementAsync(SystemRequirement sr)
        //{

        //    if(DataValidatorHelper.IsValid(sr))
        //    {
        //        var game = await SqlDataAccess.GetGameByIdAsync(sr.GameID);
        //        var platform = await SqlDataAccess.GetPlatformByIDAsync(sr.PlatformID);

        //        if (game != null && platform != null)
        //        {

        //            var srDB = await SqlDataAccess.GetSystemRequirementByGameIdAsync
        //                (sr.GameID, sr.MinimumSystemRequirement, sr.PlatformID);

        //            if (srDB == null)
        //            {

        //                return await SqlDataAccess.AddSystemRequirementAsync(sr);
        //            }

        //            return srDB.ID;
        //        }
        //    }


        //    return 0;
        //}

        //public static async Task<int> AddSteamDetailsAsync(SteamDetailsModel steamDetails)
        //{
        //    if(DataValidatorHelper.IsValid(steamDetails))
        //    {
        //        var sdDB = await SqlDataAccess.GetSteamdetailsBySteamIDAsync(steamDetails.SteamID);

        //        if(sdDB == null)
        //        {
        //            return await SqlDataAccess.AddSteamDetailsAsync(steamDetails);

        //        }
        //        return sdDB.ID;
        //    }

        //    return 0;

        //}
        //public static async Task<int> AddDealAsync(DealModel deal)
        //{

        //    if(DataValidatorHelper.IsValid(deal))
        //    {

        //        var game = await SqlDataAccess.GetGameByIdAsync(deal.GameID);
        //        var store = await SqlDataAccess.GetStoreByIDAsync(deal.StoreID);

        //        if (game != null && store != null)
        //        {
        //            var dealDB = await SqlDataAccess.

        //            GetDealByGameIdAndStoreIDAsync(deal.GameID, deal.StoreID, deal.URL);

        //            if (dealDB == null)
        //            {
        //                return await SqlDataAccess.AddDealAsync(deal);
        //            }

        //            return dealDB.ID;
        //        }
        //    }
        //    return 0;

        //}






    }
}
