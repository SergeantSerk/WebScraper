using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class SystemRequirement : ISystemRequirement
    {
        public int ID { get ; set ; }
        [Required]
        public int GameID { get ; set ; }
        [Required]
        public int PlatformID { get ; set ; }

        public Platform Platform { get; set; }
        [Required]
        public string Os { get ; set; }
        [Required]
        public string Memory { get; set; }
        [Required]
        public string Storage { get ; set ; }
        [Required]
        public string Requirement { get ; set ; }
        [Required]
        public string Processor { get ; set; }
        [Required]
        public bool MinimumSystemRequirement { get; set; }
    }
}
