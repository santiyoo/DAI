using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Pizza.API.Models;
using Pizza.API.Utils;
using Entidad = Pizza.API.Models.Pizza;
using Pizza.API.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pizza.API.Helpers{
    public static class ConfigurationHelper{
        public static IConfiguration GetConfiguration() { 
            IConfiguration config; 
            var builder = new ConfigurationBuilder() 
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange:true); 
            config = builder.Build(); 
            return config; 
        }
    }
}