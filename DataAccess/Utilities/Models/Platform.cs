
using DataAccessLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Utilities.Models
{
    public class Platform : IPlatform
    {
        public int ID { get ; set; }
        public string Title { get ; set ; }
    }
}
