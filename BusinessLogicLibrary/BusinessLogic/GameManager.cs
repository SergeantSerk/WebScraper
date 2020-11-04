using BusinessAccessLibrary.Interfaces;
using BusinessAccessLibrary.Utilities;
using DataAccessLibrary.DataAccess;
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
        private readonly ITagsDBAccess _tagDbAccess;

        public GameManager(IGameDBAccess gamedbAccess, 
            IReleaseDateDBAccess releaseDateDBAccess, 
            ISteamAppDbAccess steamAppDbAccess,
            ITagsDBAccess tagsDBAccess)
        {
            _gamedbAccess = gamedbAccess;
            _releaseDateDBAccess = releaseDateDBAccess;
            _steamAppDbAccess = steamAppDbAccess;
            _tagDbAccess = tagsDBAccess;
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
            throw new NotImplementedException();
            //var validator = DataValidatorHelper.Validate(game);


            //if(validator.IsValid)
            //{
            //    var gameDB = await GetGameByTitleAsync(game.Title);

            //    if (gameDB == null)
            //    {
            //        Console.WriteLine($"{game.Title} has been added to database");
                    
            //        return await _gamedbAccess.AddGameAsync(game);
            //    }

            //    return gameDB.GameID;
            //}


            //Console.WriteLine($"Invalid Data from {nameof(GameAddModel)}");
            //validator.Errors.ForEach(e => Console.WriteLine(e));

            //throw new Exception("Some data are invalid");
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


        public async Task<int> AddGameDeal(GameDealAddModel gameDeal)
        {
            var validator = DataValidatorHelper.Validate(gameDeal);


            if (validator.IsValid)
            {

                var storeId = await AddStore(gameDeal.Store);


                var gameDealDb = await _gamedbAccess.GetGameDealNotExpiredByStoreIdAsync
                    (gameDeal.GameId, storeId);

                if (gameDealDb == null)
                {
                    gameDeal.DealDateId = await AddDealDate(gameDeal.DealDate);
                    gameDeal.StoreId = storeId;

                    if (gameDeal.PriceOverview != null)
                    {
                        gameDeal.PriceOverviewId = await AddPriceOverview(gameDeal.PriceOverview);
                    }


                    return await _gamedbAccess.AddGameDealAsync(gameDeal);
                }

                if(gameDealDb.PriceOverviewId != null)
                {
                    UpdatePriceOverview(gameDealDb, gameDeal);
                }

                return gameDealDb.GameId;
            }


            Console.WriteLine($"Invalid Data from {nameof(GameDealAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");

        }

        public async void UpdatePriceOverview(GameDealModel gameDB, GameDealAddModel gamedeal)
        {
            
       
         if (gameDB.PriceOverviewId != 0 || gameDB.PriceOverviewId != null)
            {
               
                 var priceOverviewDB = await _gamedbAccess.GetPriceOverviewByIdAsync(gameDB.PriceOverviewId.Value);

                if (priceOverviewDB != null)
                {
                    var priceIsSame = ComparePriceIsSame(priceOverviewDB, gamedeal.PriceOverview);

                    if (!priceIsSame)
                    {
                        // exprie the current deal and add a new one
                        await _gamedbAccess.ExpireGameDealAsync(gameDB.DealDateId);
                        await AddGameDeal(gamedeal);
                    }

                }
                else
                {

                    throw new Exception("price overview does not exist in db");
                }
            }

        }


        public bool ComparePriceIsSame(PriceOverviewModel priceOverviewDB, PriceOverviewAddModel currentPriceOverview)
        {
            // check if the base price is the same
            if(priceOverviewDB.Price == currentPriceOverview.Price)
            {
                // check if the base price is the same otherwise it means price has gone on to discount
                if(priceOverviewDB.FinalPrice == currentPriceOverview.FinalPrice)
                {

                    return true;

                }

                Console.WriteLine($"The base price has changed from {priceOverviewDB.Price} to {currentPriceOverview.Price}");
                return false;

            }

            return false;
        }


        public async Task<int> AddStore(StoreAddModel store)
        {
            var validator = DataValidatorHelper.Validate(store);


            if (validator.IsValid)
            {
                var storeDb = await _gamedbAccess.GetStoreAsync(store.Name);

                if (storeDb == null)
                {

                    return await _gamedbAccess.AddStoreAsync(store);
                }

                return storeDb.StoreId;
            }


            Console.WriteLine($"Invalid Data from {nameof(StoreAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");

        }

        public async Task<int> AddPriceOverview(PriceOverviewAddModel priceOverview)
        {
            var validator = DataValidatorHelper.Validate(priceOverview);


            if (validator.IsValid)
            {


                priceOverview.CurrencyId = await AddCurrency(priceOverview.Currency);


                return await _gamedbAccess.AddPriceOverviewAsync(priceOverview);
            }


            Console.WriteLine($"Invalid Data from {nameof(PriceOverviewAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");

        }

        public async Task<int> AddCurrency(CurrencyAddModel currency)
        {
            var validator = DataValidatorHelper.Validate(currency);


            if (validator.IsValid)
            {
                var currencyDb = await _gamedbAccess.GetCurrencyByCodeAsync(currency.Code);

                if (currencyDb == null)
                {

                    return await _gamedbAccess.AddCurrencyAsync(currency);
                }

                return currencyDb.CurrencyId;
            }


            Console.WriteLine($"Invalid Data from {nameof(CurrencyAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");

        }


      




        public async Task<int> AddDealDate(DealDateAddModel deal)
        {
            var validator = DataValidatorHelper.Validate(deal);


            if (validator.IsValid)
            {

                return await _gamedbAccess.AddDealDateAsync(deal);
            }


            Console.WriteLine($"Invalid Data from {nameof(StoreAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");

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

            if(String.IsNullOrEmpty(releaseDate.ReleasedDate) && releaseDate.ComingSoon)
            {
                releaseDate.ReleasedDate = "Not Confirmed";

                return await _releaseDateDBAccess.AddReleaseDateAsync(releaseDate);

            }

            Console.WriteLine($"Invalid Data from {nameof(ReleaseDateAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");
        }

        public async Task<int> AddSystemRequirement(SystemRequirementAddModel systemRequirement)
        {

            var validator = DataValidatorHelper.Validate(systemRequirement);

            if (validator.IsValid)
            {

                systemRequirement.PlatformId = await AddPlatform(systemRequirement.Platform);

                var srDb = await _gamedbAccess.
                    GetSystemRequirementByGameIdAndPlatformIdAsync(systemRequirement.GameId, systemRequirement.PlatformId);

                if(srDb == null)
                {

                    return await _gamedbAccess.AddSystemRequirementAsync(systemRequirement);
                }
                return srDb.SystemRequirementId;
            }

            Console.WriteLine($"Invalid Data from {nameof(SystemRequirementAddModel)}");
            validator.Errors.ForEach(e => Console.WriteLine(e));

            throw new Exception("Some data are invalid");
        }

        public async Task<int> AddGamePublisherAsync(int gameId, string publisher)
        {
            if(gameId != 0 && !string.IsNullOrEmpty(publisher))
            {
                var publisherDB = await AddPublisher(publisher);

                var gamePublisherDb = await _gamedbAccess.GetGamePublisherAsync(gameId, publisherDB);

                if(gamePublisherDb  == null)
                {
                    return await _gamedbAccess.AddGamePublisherAsync(new GameAddPublisherModel
                    {
                        GameId = gameId,
                        PublisherId = publisherDB
                    });
                }


                return gamePublisherDb.PublisherId;

            }
            throw new Exception("String cannot be null or empty");
        }

        public async Task<int> AddGameDeveloperAsync(int gameId, string developer)
        {
            if (gameId != 0 && !string.IsNullOrEmpty(developer))
            {
                var developerDB = await AddDeveloper(developer);

                var gameDeveloperDb = await _gamedbAccess.GetGameDeveloperAsync(gameId, developerDB);

                if (gameDeveloperDb == null)
                {
                    return await _gamedbAccess.AddGameDeveloperAsync(new GameAddDeveloperModel
                    {
                        GameId = gameId,
                        DeveloperId = developerDB
                    });
                }


                return gameDeveloperDb.DeveloperId;

            }
            throw new Exception("String cannot be null or empty");
        }



        public async Task<int> AddPublisher(string name)

        {
            if(!string.IsNullOrEmpty(name))
            {
                var publisherDB = await _gamedbAccess.GetPublisherByNameAsync(name);

                if(publisherDB == null)
                {

                    return await _gamedbAccess.AddPublisherAsync(name);
                }

                return publisherDB.PublisherId;
            }


            throw new Exception("String cannot be null or empty");
        }


        public async Task<int> AddDeveloper(string name)

        {
            if (!string.IsNullOrEmpty(name))
            {
                var developerDB = await _gamedbAccess.GetDeveloperByNameAsync(name);

                if (developerDB == null)
                {

                    return await _gamedbAccess.AddDeveloperAsync(name);
                }

                return developerDB.DeveloperId;
            }


            throw new Exception("String cannot be null or empty");
        }

        public async Task<int> AddPlatform(PlatformAddModel platform)
        {
            var validator = DataValidatorHelper.Validate(platform);

            if (validator.IsValid)
            {
                var platformDb = await _gamedbAccess.GetPlatformByNameAsync(platform.Name);

                if(platformDb == null)
                {
                    return await _gamedbAccess.AddPlatformAsync(platform);

                }

                return platformDb.PlatformId;

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
                    Console.WriteLine($"{game.Title} has been added");

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
                else
                {
                    // check if release date is correct
                    await ValidateReleaseDate(gameDB.ReleaseDateID, game.ReleaseDate);
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


        public async Task AddGenreToGameByDescription(int gameId , string genreDescription)
        {

            if (gameId != 0 && !string.IsNullOrEmpty(genreDescription))
            {
                var genreId =  await AddGenre(genreDescription);

                var gameGenre = new GameGenreModel { GameId = gameId, GenreId = genreId };

                var genreGameDB = await _tagDbAccess.GetGameGenre(gameGenre);

                if (genreGameDB == null)
                {

                    _tagDbAccess.AddGenreToGame(gameGenre);
                }
            }
            else
            {

                throw new Exception("String cannot be null or empty");
            }
        }

        public async Task AddCategoryToGameByDescription(int gameId, string categoryDescription)
        {

            if (gameId != 0 && !string.IsNullOrEmpty(categoryDescription))
            {
                var categoryId =await AddCategory(categoryDescription);

                var categoryGame = new GameCategoryModel { GameId = gameId, CategoryId = categoryId };

                var categoryGameDB = await _tagDbAccess.GetGameCategory(categoryGame);

                if (categoryGameDB == null)
                {

                  _tagDbAccess.AddCategoryToGame(categoryGame);
                }
            }
            else
            {

                throw new Exception("String cannot be null or empty");
            }
        }


        public async Task<int> AddGenre(string genreDescription)
        {
            
            if(!string.IsNullOrEmpty(genreDescription))
            {
                var genre = await _tagDbAccess.GetGenreByDescriptionAsync(genreDescription);

                if(genre == null)
                {
                    return await _tagDbAccess.AddGenreAsync(genreDescription);
                }

                return genre.GenreId;
            }

            throw new Exception("String cannot be null or empty");
        }

        public async Task<int> AddCategory(string categoryDescription)
        {

            if (!string.IsNullOrEmpty(categoryDescription))
            {
                var categoryDB = await _tagDbAccess.GetCategoryByDescriptionAsync(categoryDescription);

                if (categoryDB == null)
                {
                    return await _tagDbAccess.AddCategoryAsync(categoryDescription);
                }

                return categoryDB.CategoryId;
            }
        

            throw new Exception("String cannot be null or empty");
        }


        public async Task ValidateReleaseDate(int? releaseDateID, ReleaseDateAddModel releaseDate)
        {
            var validator = DataValidatorHelper.Validate(releaseDate);

            if (validator.IsValid)
            {


                if (releaseDateID != null || releaseDateID != 0)
                {
                    var id = releaseDateID.Value;

                    var releaseDateDB = await _releaseDateDBAccess.GetReleaseDateByIdAsync(id);

                    if (releaseDateDB != null)
                    {
                        // check if current release data on db is true and oen passed is false then update it
                        if (releaseDateDB.ComingSoon && !releaseDate.ComingSoon ||
                            !releaseDateDB.ReleasedDate.Equals(releaseDate.ReleasedDate)
                            )
                        {

                            UpdateReleaseDate(new ReleaseDateUpdateModel
                            {
                                ReleaseDateId = releaseDateDB.ReleaseDateId,
                                ComingSoon = releaseDate.ComingSoon,
                                ReleasedDate = releaseDate.ReleasedDate

                            });
                        }
                        
                      
                    }
                    else
                    {
                        throw new Exception($"{nameof(releaseDateDB)}does not exist on DB");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Invalid Data from {nameof(ReleaseDateAddModel)}");
                validator.Errors.ForEach(e => Console.WriteLine(e));

                throw new Exception("Some data are invalid");
            }
        }


        // would need to do validation if the game/release date exist in db
        public async void UpdateReleaseDate(ReleaseDateUpdateModel releaseDateUpdate)
        {
            var validator = DataValidatorHelper.Validate(releaseDateUpdate);

            if (validator.IsValid)
            {
                _releaseDateDBAccess.UpdateReleaseDateAsync(releaseDateUpdate);
            }
            else
            {
                Console.WriteLine($"Invalid Data from {nameof(SteamAppToGameModel)}");
                validator.Errors.ForEach(e => Console.WriteLine(e));

                throw new Exception("Some data are invalid");
            }
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
