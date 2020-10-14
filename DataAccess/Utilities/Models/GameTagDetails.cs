using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class GameTagDetails:IGameTagDetails
    {

        public Tag Tag { get; set; }
        public int ID { get ; set; }
        public int GameID { get; set; }
        public int TagID { get; set ; }
    }
}
