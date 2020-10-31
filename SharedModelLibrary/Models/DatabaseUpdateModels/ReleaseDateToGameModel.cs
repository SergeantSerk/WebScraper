using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseUpdateModels
{
    public class ReleaseDateToGameModel
    {
        [Required]
        public int ReleaseDateId { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
