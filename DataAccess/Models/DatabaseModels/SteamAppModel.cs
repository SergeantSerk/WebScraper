using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models.DatabaseModels
{
    public class SteamAppModel
    {
        public int SteamAppId { get; set; }
        public string SteamReview { get; set; }
        public int SteamReviewCount { get; set; }
    }
}
