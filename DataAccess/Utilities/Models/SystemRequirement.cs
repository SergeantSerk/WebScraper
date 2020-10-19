using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class SystemRequirement : ISystemRequirement
    {
        public int ID { get ; set ; }
        public int GameID { get ; set ; }
        public int PlatformID { get ; set ; }

        public Platform Platform { get; set; }
        public string Os { get ; set; }
        public string Memory { get; set; }
        public string Storage { get ; set ; }
        public string Requirement { get ; set ; }
        public string Processor { get ; set; }
        public bool MinimumSystemRequirement { get; set; }
    }
}
