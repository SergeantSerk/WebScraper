using DataAccessLibrary.Models;
using DataAccessLibrary.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class FullGameModel: GameModel
    {


        public SteamDetailsModel? SteamDetail { get; set; }

        public List<SystemRequirement> SystemRequirements { get; set; }

        public List<DealModel> Deals { get; set; }

        public List<MediaModel> Medias { get; set; }

        public List<GameTagDetailsModel>? GameTagDetails { get; set; }

    }
}
