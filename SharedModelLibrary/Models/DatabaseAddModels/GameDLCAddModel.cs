using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class GameDLCAddModel
    {

        [Required]
        public int GameId { get; set; }
        public int DLCId { get; set; }
        [Required]
        public DLCAddModel DLC { get; set; }
    }
}
