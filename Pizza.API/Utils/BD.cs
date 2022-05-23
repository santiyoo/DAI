using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Pizza.API.Models;
using Entidad = Pizza.API.Models.Pizza;
using System. Text. Json;
using Pizza.API.Helpers;
using Microsoft.Extensions.Configuration;

namespace Pizza.API.Utils {
    public static class BD {
        //casa: DESKTOP-BO6LO81\SQLEXPRESS
        
        public static SqlConnection GetConnection(){
            string connectionString = ConfigurationHelper.GetConfiguration().GetValue<string>("DatabaseSettings:ConnectionString");

            try{    
                SqlConnection db;
                db = new SqlConnection(connectionString);
                return db;
            }
            catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}