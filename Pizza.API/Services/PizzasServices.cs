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

namespace Pizza.API.Services { //Añadir try and catch
    public static class PizzasServices {
        public static List<Entidad> GetAll() {
            List<Entidad> returnList;
            returnList = new List<Entidad>();
            string sql = "SELECT * FROM Pizzas";
            try{
                using (SqlConnection db = BD.GetConnection()) {
                    returnList = db.Query<Entidad>(sql).ToList();
                }
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "PizzasServicies", "GetAll()", sql);
                throw;
            }
            return returnList;
        }

        public static Entidad GetById(int id){
            Entidad pizza = null;
            string sp = "traerPizza";
            try{
                using (SqlConnection db = BD.GetConnection()){
                pizza = db.QueryFirstOrDefault<Entidad>(sp, new {id = id}, 
                        commandType: CommandType.StoredProcedure);
            }
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "PizzasServicies", "GetById(int id)", sp);
                throw;
            }
            return pizza;
        }

        public static int Create(Entidad pizza){
            int clavePrimaria = 0;
            string sp = "agregarPizza";
            try{
                using (SqlConnection db = BD.GetConnection()){
                clavePrimaria = db.QuerySingle<int>(sp, new {nombre = pizza.Nombre, libreGluten = pizza.LibreGluten, importe = pizza.Importe, descripcion = pizza.Descripcion}, 
                        commandType: CommandType.StoredProcedure);
                pizza.Id = clavePrimaria;
                }
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "PizzasServicies", "Create(Entidad pizza)", sp);
                throw;
            }
            return clavePrimaria;
        }

        public static int Update(Entidad pizza){
            int cambios = 0;
            string sp = "actualizarPizza";
            try{
                using(SqlConnection db = BD.GetConnection()){
                    cambios = db.Execute(sp, new {id = pizza.Id, nombre = pizza.Nombre, libreGluten = pizza.LibreGluten, importe = pizza.Importe, descripcion = pizza.Descripcion},
                            commandType: CommandType.StoredProcedure);
                }
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "PizzasServicies", "Update(Entidad pizza)", sp);
                throw;
            }
            return cambios;
        }

        public static int DeleteById(int id){
            int cambios = 0;
            string sp = "eliminarPizza";
            try{
                using(SqlConnection db = BD.GetConnection()){
                cambios = db.Execute(sp, new {id=id},
                        commandType: CommandType.StoredProcedure);
                }   
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "PizzasServicies", "DeleteById(int id)", sp);
                throw;
            }
            return cambios;
        }
    }
}