using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class PriceOverviewModel
    {
        public int PriceOverviewId { get; set; }
        public decimal Price { get; set; }
        public string PriceFormat { get; set; }
        public decimal FinalPrice { get; set; }
        public string FinalPriceFormat { get; set; }
        public int CurrencyId { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
