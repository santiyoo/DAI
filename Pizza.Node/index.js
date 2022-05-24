import { Pizza } from "./src/models/pizza.js";
import PizzaService from "./src/services/pizzas-services.js";

let srv = new PizzaService();
let todo = await srv.getAll();
console.log(todo);

let pizzaId = await srv.getById(5);
console.log(pizzaId);

// let pizzaBorrada = await srv.deleteById(1);
// console.log(pizzaBorrada);

let pizza = new Pizza()
pizza.id = 2
pizza.nombre = 'Pizza Fugazzeta'
pizza.libreGluten = true
pizza.importe = 46
pizza.descripcion = 'Chau'

let pizzaActualizada = await srv.update(pizza)
console.log(pizzaActualizada)

let pizza2 = new Pizza()
pizza2.nombre = 'Pizza X'
pizza2.libreGluten = false
pizza2.importe = 78
pizza2.descripcion = 'Pizzardix'
let pizzaNueva = await srv.insert(pizza2)
console.log(pizzaNueva)