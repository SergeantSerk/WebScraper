using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class GameTagDetailsModel:IGameTagDetails
    {

        public int ID { get ; set; }
        [Required]
        public int GameID { get; set; }
        [Required]
        public int TagID { get; set ; }

        public Tag Tag { get; set; }
    }
}
