using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Interface
{
    public interface ISystemRequirement
    {

        public int ID { get; set; }

        public int GameID { get; set; }

        public int PlatformID { get; set; }
    }
}
