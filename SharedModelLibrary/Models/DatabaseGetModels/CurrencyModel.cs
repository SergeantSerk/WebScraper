using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class CurrencyModel
    {
        public int CurrencyId { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
    }
}
