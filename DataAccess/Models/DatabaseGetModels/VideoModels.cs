using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models.DatabaseModels
{
    public class VideoModels
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public int GameId { get; set; }
        public bool Highlight { get; set; }
        public string Thumbnail { get; set; }
        public int WebmId { get; set; }
        public int MP4Id { get; set; }
    }
}
