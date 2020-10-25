using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models.DatabaseModels
{
    public class DLCModel
    {
        public int DLCId { get; set; }
        public string Title { get; set; }
        public int GameId { get; set; }
    }
}
