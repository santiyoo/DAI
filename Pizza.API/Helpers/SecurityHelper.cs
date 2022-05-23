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

namespace Pizza.API.Helpers{
    public static class SecurityHelper {
        public static string GetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool IsValidToken(string token)
        {
            return UsuariosServices.GetByToken(token) != null;
        }
    }
}