using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class ScreenshotAddModel
    {
        [Required]
        public int GameId { get; set; }
        public string PathFull { get; set; }
        public string PathThumbnail { get; set; }
    }
}
