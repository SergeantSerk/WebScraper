using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
   public class GenreAddModel
    {
        [Required]
        public string Description { get; set; }
    }
}
