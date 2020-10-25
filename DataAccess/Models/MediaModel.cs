using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class MediaModel : IMediaModel
    {
        public int ID { get; set ; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public string Url { get ; set ; }
    }
}
