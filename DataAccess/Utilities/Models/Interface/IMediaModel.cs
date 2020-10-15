using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models.Interface
{
    public interface IMediaModel
    {
        public int ID { get; set; }
        public int GameId { get; set; }
        public string Url { get; set; }
    }
}
