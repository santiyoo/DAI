using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Pizza.API.Models;
using Entidad = Pizza.API.Models.Pizza;

namespace Pizza.API.Utils {
    public static class BD {
        //casa: DESKTOP-BO6LO81\SQLEXPRESS
        private static string  _connectionString =@"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=A-BTA-08;";
        public static List<Entidad> GetAll() {
            List<Entidad> returnList;
            returnList = new List<Entidad>();

            using (SqlConnection db = new SqlConnection(_connectionString)) {
                string sql = "SELECT * FROM Pizzas";
                returnList = db.Query<Entidad>(sql).ToList();
            }
            return returnList;
        }

        public static Entidad GetById(int id){
            Entidad pizza = null;
            using (SqlConnection db = new SqlConnection(_connectionString)){
                string sp = "traerPizza";
                pizza = db.QueryFirstOrDefault<Entidad>(sp, new {id = id}, 
                        commandType: CommandType.StoredProcedure);
            }
            return pizza;
        }

        public static int Create(Entidad pizza){
            int cambios = 0;
            using (SqlConnection db = new SqlConnection(_connectionString)){
                string sp = "agregarPizza";
                cambios = db.Execute(sp, new {nombre = pizza.Nombre, libreGluten = pizza.LibreGluten, importe = pizza.Importe, descripcion = pizza.Descripcion}, 
                        commandType: CommandType.StoredProcedure);
            }
            return cambios;
        }

        public static int Update(Entidad pizza){
            int cambios = 0;
            using(SqlConnection db = new SqlConnection(_connectionString)){
                string sp = "actualizarPizza";
                cambios = db.Execute(sp, new {id = pizza.Id, nombre = pizza.Nombre, libreGluten = pizza.LibreGluten, importe = pizza.Importe, descripcion = pizza.Descripcion},
                        commandType: CommandType.StoredProcedure);
            }
            return cambios;
        }

        public static int DeleteById(int id){
            int cambios = 0;
            using(SqlConnection db = new SqlConnection(_connectionString)){
                string sp = "eliminarPizza";
                cambios = db.Execute(sp, new {id=id},
                        commandType: CommandType.StoredProcedure);
            }
            return cambios;
        }
    }
}