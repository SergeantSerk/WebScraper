using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseGetModels
{
   public class DealDateModel
    {
        public int DealDateId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime ExpiringDate { get; set; }
        public bool LimitedTimeDeal { get; set; }
        public bool Expired { get; set; }
    }
}
