import PizzaService from "./src/services/pizzas-services.js";

let srv = new PizzaService();
let todo = await srv.getAll();
console.log(todo);

let pizzaId = await srv.getById(1);
console.log(pizzaId);

let pizzaBorrada = await srv.deleteById(1);
console.log(pizzaBorrada);
