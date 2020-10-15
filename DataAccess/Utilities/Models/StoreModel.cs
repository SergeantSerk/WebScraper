using DataAccessLibrary.Utilities.Models.Interface;

namespace DataAccessLibrary.Utilities.Models
{
    public class StoreModel : IStoreModel
    {
        public int ID { get ; set; }
        public string Name { get; set ; }
        public string Logo { get; set; }
    }
}