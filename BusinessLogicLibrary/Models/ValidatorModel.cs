using BusinessAccessLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLibrary.Models
{
    public class ValidatorModel : IValidatorModel
    {
        public bool IsValid { get; set; }

        public List<string> Errors { get; set; }
    }
}
