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
using System.IO;

namespace Pizza.API.Helpers{
    public static class IOHelper { //terminar
        public static bool AppendInFile(string fullFileName, string data){
            try{    
                using (StreamWriter sw = File.AppendText(fullFileName))
                {
                    sw.WriteLine(data);
                    return true;
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}