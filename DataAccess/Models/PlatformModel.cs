
using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class PlatformModel : IPlatform
    {
        public int ID { get ; set; }
        [Required]
        public string Title { get ; set ; }
    }
}
