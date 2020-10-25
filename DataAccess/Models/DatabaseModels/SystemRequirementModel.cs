using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models.DatabaseModels
{
    public class SystemRequirementModel
    {
        public int SystemRequirementId { get; set; }
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public string Minimum { get; set; }
        public string Recommended { get; set; }
    }
}
