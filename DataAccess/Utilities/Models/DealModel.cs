using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class DealModel : IDealModel
    {
        public int ID { get; set; }
        public int GameID { get ; set ; }

        public int StoreID { get; set; }

        public StoreModel Store { get; set; }

        public decimal Price { get ; set ; }
        public decimal PreviousPrice { get; set ; }
        public bool Expired { get; set; }
        public DateTime ExpiringDate { get ; set; }
        public DateTime DatePosted { get ; set; }
        public bool LimitedTimeDeal { get; set ; }
        public string URL { get ; set; }
        public bool IsFree { get; set; }
    }
}
