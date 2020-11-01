using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class PlatformAddModel
    {
        [Required]
        public string Name { get; set; }
    }
}
