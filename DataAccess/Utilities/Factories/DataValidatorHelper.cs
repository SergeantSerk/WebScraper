using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.BusinessLogic
{
    public static class DataValidatorHelper
    {
        public static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);

            var results = new List<ValidationResult>();

            //results.ForEach(r => Console.WriteLine(r.ErrorMessage));

            return Validator.TryValidateObject(obj, context, results, true);
        }

    }
}