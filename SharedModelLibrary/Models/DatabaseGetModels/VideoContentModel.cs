using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseModels
{
    public class VideoContentModel
    {
        public int VideoContentId { get; set; }
        public string   Quality { get; set; }
        public string Max { get; set; }
        public string MediaType { get; set; }
    }
}
