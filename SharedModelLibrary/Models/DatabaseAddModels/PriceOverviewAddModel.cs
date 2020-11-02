using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
   public class PriceOverviewAddModel
    {

        [Required]
        public decimal Price { get; set; }
        public string PriceFormat { get; set; }
        public decimal FinalPrice { get; set; }
        public string FinalPriceFormat { get; set; }
        public int CurrencyId { get; set; }
        [Required]
        public CurrencyAddModel Currency { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
