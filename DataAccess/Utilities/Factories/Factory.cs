
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Factories
{
    public static class Factory
    {

        public static IConfiguration getConfiguration(string appsetting)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                  .AddJsonFile(appsetting, true, true)
                  .Build();

            return configuration;
        }

        public static IConfiguration getConfiguration()
        {
            var appsetting = @"C:\Users\Abdul\source\repos\Webscraper\DataAccess\appsettings.json";

            IConfiguration configuration = new ConfigurationBuilder()
                  .AddJsonFile(appsetting, true, true)
                  .Build();

            return configuration;
        }

     

    }
}
