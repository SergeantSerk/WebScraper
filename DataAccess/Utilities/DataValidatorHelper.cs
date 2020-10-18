using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccessLibrary.Utilities
{
    public static class DataValidatorHelper
    {
      

        private static List<string> _properiesToIgnore = new List<string>() { "ID".ToLower() };


        public static void AddPropertiesToIgnore(string property)
        {
            _properiesToIgnore.Add(property);
        }

        public static bool HasAllEmptyProperties(object obj)
        {
           

            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var p = prop;
                var propValue = p.GetValue(obj);


                if (_properiesToIgnore.Contains(p.Name.ToLower()))
                {
                    continue;
                }


                if (propValue == null ||String.IsNullOrWhiteSpace(propValue.ToString()))
                {

                    return false;
                }


            }

            return true;
        }


          
        
      
    }
}
