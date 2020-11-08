using BusinessAccessLibrary.Interfaces;
using SharedModelLibrary.Models.DatabaseAddModels;
using SharedModelLibrary.Models.DatabasePostModels;
using Steam.Interfaces;
using Steam.Models;
using Steam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private SteamProfileSettings _steamProfileSettings; 



        public SteamProcessors(ISteamAPI steamAPI, IGameManager gameManager,  int delayTime = 1500)
        {
            _steamAPI = steamAPI;
            _requestDelayTime = delayTime;
            _gameManager = gameManager;
            _requestDelayTime = delayTime;
        }

   
        public async Task Start()
        {
            //await setupSteamProfileIndex();
            _apps = await _steamAPI.GetApps();
            _totalApps =_apps.Count;
            await Process();
        }

        private async Task Process()
        {
            _steamProfileSettings = await SteamProfileSettingsUtilitie.GetSteamProfileSettingsAsync();


            var steamAppIdsFromDB = await _gameManager.GetAllSteamIdAsync();

            var set = new HashSet<int>(steamAppIdsFromDB);

            _apps.RemoveAll(i => set.Contains(i.appid));

            for (var i = _currentIndex; i < _apps.Count; i++)
            {

                var app = _apps[i];
                var fullApp = await _steamAPI.GetAppBySteamID(app.appid);

                if(fullApp != null)
                {
                    AddFullGame(fullApp);
                    updateSteamProfileIndexByOneSettingAsync();
                }
                else
                {
                    Console.WriteLine($"No Data for {app.appid}");
                    _gameManager.AddSteamApp(new SteamAppAddModel { 
                        SteamAppId = app.appid,
                        Valid = false

                    });
                }

                await Task.Delay(_requestDelayTime);

            }




            Console.WriteLine("Database has finished");
        }


        //private async void GatherFullData()
        //{

        //    for (var i = _currentIndex; i < _totalApps; i++)
        //    {
        //        var app = _apps[i];

        //        //var fullApp = await _steamAPI.GetAppBySteamID(app.appid);
        //        var fullApp = await _steamAPI.GetAppBySteamID(app.appid);
        //        await Task.Delay(_requestDelayTime);


        //        if (fullApp != null)
        //        {
        //            if (!String.IsNullOrEmpty(fullApp.Name))
        //            {


        //                AddFullGame(fullApp);
        //                updateSteamProfileIndexByOneSettingAsync();
        //            }
        //        }
        //    }
        //    Console.WriteLine("Database has finished");
        //}


        private async void AddFullGame(SteamAppDetails fullApp)
        {
            var gameId = await AddGame(fullApp);

            /// all synchronous methods best to get results or await 
            /// to figure out the Exception thrown: 'System.Data.SqlClient.SqlException' in System.Private.CoreLib.dll
            /// as expections are thrown but not picked up


            AddDlcAsync(fullApp.DLC, gameId);
            addDeveloper(fullApp.Developers, gameId);
            addPublisher(fullApp.Publishers, gameId);
            AddVideos(fullApp.Movies, gameId, fullApp.Name);

            addGameDeal(fullApp, gameId);

            AddCategories(fullApp.Categories, gameId);
            AddGenre(fullApp.Genres, gameId);
            addSystemRequirements(fullApp, gameId);
        }

        private async void AddDlcAsync(List<int> Dlcs, int gameId)
        {
            if(Dlcs != null)
            {
                foreach(var dlcId in Dlcs)
                {
                    await _gameManager.AddGameDLC(new GameDLCAddModel
                    {
                        GameId = gameId,
                        DLC = new DLCAddModel { SteamAppId = dlcId}
                    });
                }
            }

        }
        private async void AddVideos(List<Movie> movies, int gameId, string title)
        {
            if(movies != null)
            {
                foreach (var video in movies)
                {
                    var videoToAdd = new VideoAddModel()
                    {
                        GameId = gameId,
                        Title = string.IsNullOrEmpty(video.Name)? title : video.Name ,
                        Thumbnail = video.Thumbnail
                    };

                    if (video.MP4 != null)
                        videoToAdd.MP4 = new VideoContentAddModel
                        {
                            Max = video.MP4.Max,
                            Quality = video.MP4.Quality,
                            MediaType = "mp4"
                        };

                    if(video.Webm != null)
                        videoToAdd.Webm = new VideoContentAddModel
                        {
                            Max = video.Webm.Max,
                            Quality = video.Webm.Quality,
                            MediaType = "webm"
                        };
            

                   await _gameManager.AddVideoAsync(videoToAdd);
                }
            }
        }
        //private async Task setupSteamProfileIndex()
        //{
        //    var steamApp = await _gameManager.GetAllSteamIdAsync();
        //    var g = steamApp.ToList();
        //    if (g.Count == 0)
        //        resetSteamProfile();
        //    _steamProfileSettings = await SteamProfileSettingsUtilitie.GetSteamProfileSettingsAsync();
        //    _currentIndex = _steamProfileSettings.CurrentIndex;


        //}

        private async void resetSteamProfile()
        {
            SteamProfileSettingsUtilitie.UpdateSteamProfileSettingAsync(new SteamProfileSettings { });
        }

        private async void updateSteamProfileIndexByOneSettingAsync()
        {
            _steamProfileSettings.CurrentIndex++;
            _steamProfileSettings.PreviousIndex = _steamProfileSettings.CurrentIndex - 1;

            SteamProfileSettingsUtilitie.UpdateSteamProfileSettingAsync(_steamProfileSettings);

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
           _gameManager.AddGameDeal(gamedeal);
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
            var releasedDate = fullApp.ReleaseDate.ReleaseDate;

            var fullGameModel = new FullGameAddModel
            {
                Title = fullApp.Name,
                Type = fullApp.Type,
                Website = fullApp.Website,
                Description = fullApp.Description,
                HeaderImage = fullApp.HeaderImage,
                Background = fullApp.Background,
                About = fullApp.About,
                ShortDescription = fullApp.ShortDescription,
                ReleaseDate = new ReleaseDateAddModel()
                {
                    ComingSoon = fullApp.ReleaseDate.ComingSoon,
                    ReleasedDate = String.IsNullOrEmpty(releasedDate) ? "Not confirmed" : releasedDate 

                },
                steamApp = new SteamAppAddModel()
                {
                    SteamAppId = fullApp.SteamAppID,
                    SteamReview = fullApp.Reviews,
                    Valid = true
                }
        };




            return await _gameManager.AddFullGameAsync(fullGameModel);

            
        }




    }
}
