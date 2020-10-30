using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class DealModel
    {
        public int DealId { get; set; }
        public string URL { get; set; }
        public bool IsFree { get; set; }
        public int StoreId { get; set; }
        public int GameId { get; set; }
        public bool Expired { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime ExpiringDate { get; set; }
        public int PriceOverviewId { get; set; }
        public bool LimitedTimeDeal { get; set; }
    }
}
