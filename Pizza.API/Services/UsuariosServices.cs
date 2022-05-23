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
using Pizza.API.Helpers;

namespace Pizza.API.Services{
    public static class UsuariosServices {
        public static Usuario Login(string userName, string password){
            Usuario entidad = GetByUserNamePassword(userName, password);
            string token; 
            // entidad = GetByUserNamePassword(userName, password);
            if(entidad != null){
                token = RefreshToken(entidad.Id);
                if(token != null){
                    entidad = GetByUserNamePassword(userName, password);
                }
            }
            return entidad;
        }

        public static Usuario GetByUserNamePassword(string userName, string password){
            Usuario usuario = null;
            string sql = "SELECT * FROM Usuarios WHERE userName = @userName AND password = @password";
            try{
                using (SqlConnection db = BD.GetConnection()){
                    usuario = db.QueryFirstOrDefault<Usuario>(sql, new {userName = userName, password = password});
                }
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "UsuariosServices", "GetByUserNamePassword(string userName, string password)", sql);
                throw;
            }
            return usuario;
        }

        public static Usuario GetByToken(string token){
            Usuario usuario = null;
            string sql = "SELECT * FROM Usuariox WHERE token = @ptoken";
            try{
                using (SqlConnection db = BD.GetConnection()){
                    usuario = db.QueryFirstOrDefault<Usuario>(sql, new {ptoken = token});
                }
            }    
            catch(Exception ex){
                CustomLog.LogError(ex, "UsuariosServices", "GetByToken(string token)", sql);
                throw;
            }
            return usuario;
        }

        private static string RefreshToken(int id){
            DateTime tokenExpirationDate = DateTime.Now.AddMinutes(15);
            string sql = "UPDATE Usuarios SET token = @token, tokenExpirationDate = @tokenExpirationDate WHERE id = @id";
            string token = SecurityHelper.GetToken();
            try{
                using (SqlConnection db = BD.GetConnection()){
                    db.Execute(sql, new {token, tokenExpirationDate, id});
                }
            }
            catch(Exception ex){
                CustomLog.LogError(ex, "UsuariosServices", "RefreshToken(int id)", sql);
                throw;
            }
            return token;
        }
    }
}