using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModelLibrary.Models.DatabaseAddModels
{
    public class DLCAddModel
    {
        [Required]
        public int SteamAppId { get; set; }
        public string Title { get; set; }
    }
}
