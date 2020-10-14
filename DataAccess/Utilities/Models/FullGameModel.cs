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

        public List<GameTagDetails>? GameTagDetails { get; set; }

    }
}
