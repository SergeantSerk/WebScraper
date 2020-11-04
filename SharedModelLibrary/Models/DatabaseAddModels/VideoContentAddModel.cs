using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
   public class VideoContentAddModel
    {
        [Required]
        public string Quality { get; set; }
        [Required]
        public string Max { get; set; }
        [Required]
        public string MediaType { get; set; }
    }
}
