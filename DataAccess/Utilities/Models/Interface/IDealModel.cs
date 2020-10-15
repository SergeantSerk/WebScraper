using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models.Interface
{
    public interface IDealModel
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int StoreID { get; set; }
        public Decimal Price { get; set; }
        public Decimal PreviousPrice { get; set; }
        public bool Expired { get; set; }
        public DateTime ExpiringDate { get; set; }
        public DateTime DatePosted { get; set; }
        public bool LimitedTimeDeal { get; set; }
        public string URL { get; set; }

        public bool IsFree { get; set; }

    }
}
