using BusinessAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabasePostModels;
using Steam.Interfaces;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Processors
{
    public class SteamProcessors
    {
        private  int _currentIndex;
        private readonly ISteamAPI _steamAPI;
        private int _totalApps;
        private List<SteamApp> _apps;
        private readonly int _requestDelayTime;
        private readonly IGameManager _gameManager;
        private List<Task> tasks = new List<Task>();
        private const string _storeName = "steam";
        private const string _basedStoreURl = "https://store.steampowered.com/app/";



        public SteamProcessors(ISteamAPI steamAPI, IGameManager gameManager,  int delayTime = 1500)
        {
            _steamAPI = steamAPI;
            _requestDelayTime = delayTime;
            _gameManager = gameManager;
            _requestDelayTime = delayTime;
        }

        private async Task<int> GetCurrentIndex()
        {
            return 5;
        }

        private async  Task<string> ReadSettingJson()
        {
            throw new NotImplementedException();
        }

        public async Task Start()
        {
            _currentIndex = 6;
            _apps = await _steamAPI.GetApps();
            _totalApps =1000;
            await Process();
        }

        private async Task Process()
        {
           
            for(var i = _currentIndex; i < _totalApps; i++ )
            {
                var app = _apps[i];

                var fullApp = await _steamAPI.GetAppBySteamID(app.appid);


                if (fullApp != null)
                {
                   var gameId =  await AddGame(fullApp);

                    /// all synchronous methods best to get results or await 
                    /// to figure out the Exception thrown: 'System.Data.SqlClient.SqlException' in System.Private.CoreLib.dll
                    /// as expections are thrown but not picked up



                    addDeveloper(fullApp.Developers, gameId);
                    addPublisher(fullApp.Publishers, gameId);

                    addGameDeal(fullApp, gameId);

                    AddCategories(fullApp.Categories, gameId);
                    AddGenre(fullApp.Genres, gameId);
                    addSystemRequirements(fullApp, gameId);
                    await Task.Delay(_requestDelayTime);

                }
            }
        }

        private void addPublisher(List<string> publishers, int gameId)
        {
            if (publishers != null)
            {
                foreach(var publisher in publishers)
                {
                    if (!string.IsNullOrEmpty(publisher))
                    {
                        var t = _gameManager.AddGamePublisherAsync(gameId,
                          publisher).Result;
                    }
                }

            }
               
        }
        private void addDeveloper(List<string> developers, int gameId)
        {
            if(developers != null)
            {
                foreach (var developer in developers)
                {
                    if (!string.IsNullOrEmpty(developer))
                    {
                        var t = _gameManager.AddGameDeveloperAsync(gameId, developer).Result;
                    }
                }
            }
                  

        }
        private void addGameDeal(SteamAppDetails app, int gameId)
        {
            DateTime currentDateTime = DateTime.Now;
            string sqlFormattedDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");


            var gamedeal = new GameDealAddModel
            {
                GameId = gameId,
                DealDate = new DealDateAddModel { DatePosted = sqlFormattedDate },
                Url = _basedStoreURl + app.SteamAppID,
                IsFree = app.IsFree,
                Store = new StoreAddModel { Name = _storeName },
               
            };

            var priceOverview = app.PriceOverview;

            if (priceOverview != null)
            {
                gamedeal.PriceOverview = new PriceOverviewAddModel
                {
                    Price = priceOverview.Initial,
                    PriceFormat = priceOverview.InitialFormat,
                    FinalPrice = priceOverview.Final,
                    FinalPriceFormat = priceOverview.FinalFormat,
                    DiscountPercentage = priceOverview.DiscountPercentage,
                    Currency = new CurrencyAddModel { Code = priceOverview.Currency, Symbole = "£" }
                };

               if(priceOverview.Initial != priceOverview.Final)
                {
                    gamedeal.DealDate.LimitedTimeDeal = true;
                }
            }
          var t =   _gameManager.AddGameDeal(gamedeal).Result;
        }

        private void addSystemRequirements(SteamAppDetails systemRequirement, int gameId)
        {
            if (systemRequirement.PcRequirement != null)
            {
             _gameManager.AddSystemRequirement(new SystemRequirementAddModel
                {
                    GameId = gameId,
                    Minimum = systemRequirement.PcRequirement.Minimum,
                    Recommended = systemRequirement.PcRequirement.Recommended,
                    Platform = new PlatformAddModel { Name = "pc" }
                });
            }
            if (systemRequirement.LinuxRequirement != null)
            {
                _gameManager.AddSystemRequirement(new SystemRequirementAddModel
                {
                    GameId = gameId,
                    Minimum = systemRequirement.LinuxRequirement.Minimum,
                    Recommended = systemRequirement.LinuxRequirement.Recommended,
                    Platform = new PlatformAddModel { Name = "linux" }
                });
            }
            if (systemRequirement.MacRequirement != null)
            {
                _gameManager.AddSystemRequirement(new SystemRequirementAddModel
                {
                    GameId = gameId,
                    Minimum = systemRequirement.MacRequirement.Minimum,
                    Recommended = systemRequirement.MacRequirement.Recommended,
                    Platform = new PlatformAddModel { Name = "mac" }
                });
            }

        }

        private void AddCategories(List<CategoryModel> categories, int gameId)
        {
            if (categories != null)
            {
                foreach (var category in categories)
                {
                     _gameManager.AddCategoryToGameByDescription(gameId, category.Description);
                };
            }
        }

        private void AddGenre(List<GenreModel> genres, int gameId)
        {
            if (genres != null)
            {
                foreach (var genre in genres)
                {
                    _gameManager.AddGenreToGameByDescription(gameId, genre.Description);
                };
            }
        }

        private async Task<int> AddGame (SteamAppDetails fullApp)
        {
            var fullGameModel = new FullGameAddModel
            {
                Title = fullApp.Name,
                Type = fullApp.Type,
                Website = fullApp.Website,
                Description = fullApp.Description,
                HeaderImage = fullApp.HeaderImage,
                Background = fullApp.Background,
                ReleaseDate = new ReleaseDateAddModel()
                {
                    ComingSoon = fullApp.ReleaseDate.ComingSoon,
                    ReleasedDate = fullApp.ReleaseDate.ReleaseDate

                },
                steamApp = new SteamAppAddModel()
                {
                    SteamAppId = fullApp.SteamAppID,
                    SteamReview = fullApp.Reviews,
                }
        };




            return await _gameManager.AddFullGameAsync(fullGameModel);

            
        }




    }
}
