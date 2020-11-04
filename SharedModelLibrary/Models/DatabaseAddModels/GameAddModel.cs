using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabasePostModels
{
    public class GameAddModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        public string About { get; set; }
        public string Website { get; set; }
        public string Thumbnail { get; set; }
        public string ShortDescription { get; set; }

        public string Description { get; set; }
        [Required]
        public string HeaderImage { get; set; }
        public string Background { get; set; }

    }
}
