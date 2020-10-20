using DataAccessLibrary.Utilities.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Utilities.Models
{
    public class StoreModel : IStoreModel
    {
        public int ID { get ; set; }
        [Required]
        public string Name { get; set ; }
        [Required]
        public string Logo { get; set; }
    }
}