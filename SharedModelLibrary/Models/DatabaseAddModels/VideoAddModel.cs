using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class VideoAddModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int GameId { get; set; }
        public bool Highlight { get; set; }
        public string Thumbnail { get; set; }
        public int? WebmId { get; set; }
        public VideoContentAddModel Webm { get; set; }
        public VideoContentAddModel MP4 { get; set; }
        public int? MP4Id { get; set; }
    }
}
