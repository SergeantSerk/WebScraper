using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class ReleaseDateModel
    {
        [Required]
        public int ReleaseDateId { get; set; }
        [Required]
        public bool ComingSoon { get; set; }
        [Required]
        public string ReleasedDate { get; set; }
    }
}
