import PizzaService from "./src/services/pizzas-services.js";

let srv = new PizzaService();
let todo = await srv.getAll();
console.log(todo);

let pizzaId = await srv.getById(1);
console.log(pizzaId);

<<<<<<< HEAD
=======
let pizzaBorrada = await srv.deleteById(1);
console.log(pizzaBorrada);
>>>>>>> 10d75dc9b590e30a747894a97346641cf2671c0c
