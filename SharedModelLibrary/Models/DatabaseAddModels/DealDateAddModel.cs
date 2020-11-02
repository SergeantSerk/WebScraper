using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class DealDateAddModel
    {   [Required]
        public string DatePosted { get; set; }
        public string ExpiringDate { get; set; }
        public bool LimitedTimeDeal { get; set; }
        public bool Expired { get; set; } = false;
    }
}
