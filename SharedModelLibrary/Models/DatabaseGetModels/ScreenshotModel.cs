using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class ScreenshotModel
    {
        public int ScreenshotId { get; set; }
        public int GameId { get; set; }
        public string PathFull { get; set; }
        public string PathThumbnail { get; set; }
    }
}
