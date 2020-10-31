using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class GameModel 
    {
        public int GameID { get; set; }
        
        public string Title { get; set; }
        public string Type { get; set; }
        public string About { get; set; }
        public string Website { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int? ReleaseDateID { get; set; }
        public string HeaderImage { get; set; }
        public string Background { get; set; }
        public int? SteamAppId { get; set; }




    }
}
