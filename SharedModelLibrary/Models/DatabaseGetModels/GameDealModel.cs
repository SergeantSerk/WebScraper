using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class GameDealModel
    {
        public string Url { get; set; }
        public int StoreId { get; set; }
        public int GameId { get; set; }
        public int? PriceOverviewId { get; set; }
        public int DealDateId { get; set; }
        public bool IsFree { get; set; }

    }
}
