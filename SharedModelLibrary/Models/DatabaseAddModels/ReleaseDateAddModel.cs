using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class ReleaseDateAddModel
    {

        public bool ComingSoon { get; set; }
        [Required]
        public string ReleasedDate { get; set; }
    }
}
