using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class GameModel : IGameModel
    {
        public int ID { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }


        public string About { get; set; }

        [Required]
        public string Thumbnail { get; set; }
        [Required]
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public int SteamDetailsID { get; set; }

    }
}
