using DataAccessLibrary.Interface;

namespace DataAccessLibrary.Utilities.Models
{
    public class Tag : ITag
    {

        public int ID { get; set; }

        public string Title { get; set; }
    }
}