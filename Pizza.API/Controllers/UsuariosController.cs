using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizza.API.Models;
using Pizza.API.Utils;
using Pizza.API.Services;
using System.IO;

namespace Pizza.API.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsuariosController : ControllerBase{
        [HttpPost]
        [Route("login")] 
        public IActionResult Login(Usuario usuario){
            Usuario UsuarioLogin = UsuariosServices.Login(usuario.UserName, usuario.Password);
            try{
                if(UsuarioLogin == null){
                    return NotFound();
                }
                else{
                    return Ok(UsuarioLogin.Token);
                }   
            }
            catch(Exception){
                throw;
            }
        }       
    }
}