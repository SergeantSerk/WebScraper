using SharedModelLibrary.Models.DatabaseGetModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
   public class GameDealAddModel
    {
        [Required]
        public string Url { get; set; }
        
        public int StoreId { get; set; }
        [Required]
        public int GameId { get; set; }
        public int? PriceOverviewId { get; set; }
        public int DealDateId { get; set; }

        [Required]
        public StoreAddModel Store { get; set; }

        [Required]
        public DealDateAddModel DealDate { get; set; }

        public PriceOverviewAddModel PriceOverview { get; set; }

        public bool IsFree { get; set; } = false;
    }
}
