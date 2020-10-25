using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models.DatabaseModels
{
    public class PublisherModel
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
    }
}
