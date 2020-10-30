using BusinessAccessLibrary.Interfaces;
using BusinessAccessLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BusinessAccessLibrary.Utilities
{
    public static class DataValidatorHelper
    {

        public static IValidatorModel Validate(object obj)
        {
            var context = new ValidationContext(obj);

            var results = new List<ValidationResult>();

            //results.ForEach(r => Console.WriteLine(r.ErrorMessage));

            var isValidObject = Validator.TryValidateObject(obj, context, results, true);


            return new ValidatorModel()
            {
                IsValid = isValidObject,
                Errors = results.Select(r => r.ErrorMessage).ToList()
        };


            
        }

    }
}