using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Pizza.API.Models;
using Entidad = Pizza.API.Models.Pizza;
using System.Text.Json;
using Pizza.API.Helpers;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Pizza.API.Utils {
    public static class CustomLog {
        //casa: DESKTOP-BO6LO81\SQLEXPRESS
        
        public static void LogError(Exception ex){
            LogError(ex.ToString(), null, null, null);
        } 

        public static void LogError (string errorData) {
            LogError(errorData, null, null, null);
        }

        public static void LogError(Exception ex, string className, string functionName){
            LogError(ex.Message, className, functionName, null);
        }

        public static void LogError(Exception ex, string className, string contexto, object datos) {
            LogError(ex.ToString(), className, contexto, datos);
        }
        
       public static void LogError (string errorData, string className, string contexto, object datos) {

            // string data, dataString = "";
            // if(datos != null) {
            //     dataString = JsonConvert.SerializeObject(datos);
            // }

            string data = "";

            data = string.Format("{0} {1}{2}{3}{4}", 
                    DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"),
                    errorData,
                    (className != null) ? $"\n\tClassName\t= {className}" : "",
                    (contexto != null) ? $"\n\tContexto\t= {contexto}" : "",
                    (datos != null) ? $"\n\tClassName\t= {datos}" : ""
            );

            try
            {
                string path = ConfigurationHelper.GetConfiguration()["CustomLog:LogFolder"];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fullFileName = ConfigurationHelper.GetConfiguration()["CustomLog:LogFolder"] + @"\log.txt";
                IOHelper.AppendInFile(fullFileName, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}