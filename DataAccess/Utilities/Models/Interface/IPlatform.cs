using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Interface
{
    interface IPlatform
    {
        public int ID { get; set; }

        public string Title { get; set; }
    }
}
