using Dapper;
using DataAccessLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess.DBAccessFactory
{
    public static class DALFactory
    {


        private const string _appSettingPath =
           @"C:\Users\Abdul\source\repos\Webscraper\DataAccess\appsettings.json";


        public static IGameDBAccess GetGameDBAccess()
        {
            return new GameDBAccess();
        }


        public static IConfiguration getConfiguration(string appsetting)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                  .AddJsonFile(appsetting, true, true)
                  .Build();

            return configuration;
        }

        public static IConfiguration getConfiguration()
        {


            IConfiguration configuration = new ConfigurationBuilder()
                  .AddJsonFile(_appSettingPath, true, true)
                  .Build();

            return configuration;
        }

      


    }
}
