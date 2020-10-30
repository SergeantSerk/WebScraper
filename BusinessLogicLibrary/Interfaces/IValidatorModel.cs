using System.Collections.Generic;

namespace BusinessAccessLibrary.Interfaces
{
    public interface IValidatorModel
    {
        List<string> Errors { get; set; }
        bool IsValid { get; set; }
    }
}