using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
   public class SteamAppAddModel
    {
        [Required]
        public int SteamAppId { get; set; }
        public string SteamReview { get; set; }
        public int SteamReviewCount { get; set; }
    }
}
