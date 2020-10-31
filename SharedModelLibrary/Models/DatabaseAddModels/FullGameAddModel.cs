using SharedModelLibrary.Models.DatabasePostModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class FullGameAddModel:GameAddModel
    {
        public int SteamAppId { get; set; }

        public int ReleaseDateId { get; set; }

        [Required]
        public ReleaseDateAddModel ReleaseDate { get; set; }
        [Required]
        public SteamAppAddModel steamApp { get; set; }
    }
}
