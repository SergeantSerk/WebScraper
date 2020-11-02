using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
   public class StoreAddModel
    {
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}
