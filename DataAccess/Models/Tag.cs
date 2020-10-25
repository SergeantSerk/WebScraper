using DataAccessLibrary.Interface;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Utilities.Models
{
    public class Tag : ITag
    {

        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
    }
}