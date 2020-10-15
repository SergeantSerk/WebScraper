using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class MediaModel : IMediaModel
    {
        public int ID { get; set ; }
        public int GameId { get; set; }
        public string Url { get ; set ; }
    }
}
