using DataAccessLibrary.Utilities.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class DealModel : IDealModel
    {
        public int ID { get; set; }
        [Required]
        public int GameID { get ; set ; }
        [Required]
        public int StoreID { get; set; }

        public StoreModel Store { get; set; }
        [Required]
        public decimal Price { get ; set ; }
        
        public decimal PreviousPrice { get; set ; }
        [Required]
        public bool Expired { get; set; }
        
        public DateTime ExpiringDate { get ; set; }
        [Required]
        public DateTime DatePosted { get ; set; }
        [Required]
        public bool LimitedTimeDeal { get; set ; }
        [Required]
        public string URL { get ; set; }
        [Required]
        public bool IsFree { get; set; }
    }
}
