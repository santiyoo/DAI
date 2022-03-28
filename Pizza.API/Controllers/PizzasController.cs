using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizza.API.Models;
using Pizza.API.Utils;

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

            _listaPizzas = BD.GetAll(); 
            respuesta = Ok(_listaPizzas);
            return respuesta;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            IActionResult respuesta;
            Pizza.API.Models.Pizza _pizza = BD.GetById(id);
            if(_pizza==null){
                respuesta = NotFound();
            }
            else{
                respuesta = Ok(_pizza);
            }
            return respuesta;
        }

        [HttpPost]
        public IActionResult Create(Pizza.API.Models.Pizza pizza){
            int creacion = BD.Create(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza); 
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza.API.Models.Pizza pizza){
            IActionResult respuesta = null;
            Pizza.API.Models.Pizza entity;
            int cambios;
            if(id != pizza.Id){
                respuesta = BadRequest();
            }
            else{
                entity = BD.GetById(id);
                if(entity == null){
                    respuesta = NotFound();
                }
                else{
                    cambios = BD.Update(pizza);
                    if (cambios > 0){ 
                        respuesta = Ok(pizza); 
                    } 
                    else { 
                        respuesta = NotFound(); 
                    }
                }
            }
            return respuesta;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id){
            IActionResult respuesta = null;
            Pizza.API.Models.Pizza entity;
            int cambios;
            entity = BD.GetById(id);

            if( entity == null){
                respuesta = NotFound();
            }
            else{
                cambios = BD.DeleteById(id);
                if (cambios > 0){
                    respuesta = Ok(entity);
                } else {
                    respuesta = NotFound();
                }
            }
            return respuesta; 
        }
    }
}

