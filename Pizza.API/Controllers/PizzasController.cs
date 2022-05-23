using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizza.API.Models;
using Pizza.API.Utils;
using Pizza.API.Services;
using Pizza.API.Helpers;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;

namespace Pizza.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(){
            IActionResult respuesta;
            List<Pizza.API.Models.Pizza> _listaPizzas; 
            try{
                _listaPizzas = PizzasServices.GetAll(); 
                respuesta = Ok(_listaPizzas);
                return respuesta;
            }
            catch(Exception ex){
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                respuesta = Problem("Hubo un - Internal Server Error!!", 
                HttpContext.Request.GetDisplayUrl(), 
                StatusCodes.Status500InternalServerError, 
                "Surgió un Error", "Http://www.problemas-resolver.com/error-delete" );
                return respuesta;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            IActionResult respuesta;
            Pizza.API.Models.Pizza _pizza = PizzasServices.GetById(id);
            try{
                if(_pizza==null){
                    respuesta = NotFound();
                }
                else{
                    respuesta = Ok(_pizza);
                }
                return respuesta;
            }
            catch(Exception ex){
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                respuesta = Problem("Hubo un - Internal Server Error!!", 
                        HttpContext.Request.GetDisplayUrl(), 
                        StatusCodes.Status500InternalServerError, 
                        "Surgió un Error", "Http://www.problemas-resolver.com/error-delete");
                return respuesta;
            }
        }

        [HttpPost]
        public IActionResult Create(Pizza.API.Models.Pizza pizza){
            IActionResult respuesta = null;
            int cambios;
            string headerToken; 
            headerToken = Request.Headers["token"];
            try{
                if(SecurityHelper.IsValidToken(headerToken)){
                    cambios = PizzasServices.Create(pizza);
                    respuesta = CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
                }
                else{
                    respuesta = Unauthorized();
                } 
                return respuesta;
                }
            catch(Exception ex){
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                respuesta = Problem("Hubo un - Internal Server Error!!", 
                        HttpContext.Request.GetDisplayUrl(), 
                        StatusCodes.Status500InternalServerError, 
                        "Surgió un Error", "Http://www.problemas-resolver.com/error-delete" );
                return respuesta;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza.API.Models.Pizza pizza){
            IActionResult respuesta = null;
            Pizza.API.Models.Pizza entity;
            int cambios;
            string headerToken; 
            headerToken = Request.Headers["token"];
            try{
                if(SecurityHelper.IsValidToken(headerToken)){
                    if(id != pizza.Id){
                        respuesta = BadRequest();
                    }
                    else{
                        entity = PizzasServices.GetById(id);
                        if(entity == null){
                            respuesta = NotFound();
                        }
                        else{
                            cambios = PizzasServices.Update(pizza);
                            if (cambios > 0){ 
                                respuesta = Ok(pizza); 
                            } 
                            else { 
                                respuesta = NotFound(); 
                            }
                        }
                    }
                }
                else{
                    respuesta = Unauthorized();
                }
                return respuesta;
            }
            catch(Exception ex){
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                respuesta = Problem("Hubo un - Internal Server Error!!", 
                        HttpContext.Request.GetDisplayUrl(), 
                        StatusCodes.Status500InternalServerError, 
                        "Surgió un Error", "Http://www.problemas-resolver.com/error-delete" );
                return respuesta;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id){
            IActionResult respuesta = null;
            Pizza.API.Models.Pizza entity;
            int cambios;
            entity = PizzasServices.GetById(id);
            string headerToken; 
            headerToken = Request.Headers["token"];
            try{
                if(SecurityHelper.IsValidToken(headerToken)){
                    if( entity == null){
                        respuesta = NotFound();
                    }
                    else{
                        cambios = PizzasServices.DeleteById(id);
                        if (cambios > 0){
                            respuesta = Ok(entity);
                        } else {
                            respuesta = NotFound();
                        }
                    }
                }
                else{
                    respuesta = Unauthorized();
                }
                return respuesta; 
            }
            catch(Exception ex){
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                respuesta = Problem("Hubo un - Internal Server Error!!", 
                        HttpContext.Request.GetDisplayUrl(), 
                        StatusCodes.Status500InternalServerError, 
                        "Surgió un Error", "Http://www.problemas-resolver.com/error-delete" );
                return respuesta;
            }
        }
    }
}

