﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models.DatabaseModels
{
    public class CurrencyModel
    {
        public int CurrencyId { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
    }
}