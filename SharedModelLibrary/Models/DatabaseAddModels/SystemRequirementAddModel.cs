using SharedModelLibrary.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class SystemRequirementAddModel
    {
        [Required]
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public string Minimum { get; set; }
        public string Recommended { get; set; }
        [Required]
        public PlatformAddModel Platform { get; set; }
    }
}
