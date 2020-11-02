using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class CurrencyAddModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Symbole { get; set; }
    }
}
