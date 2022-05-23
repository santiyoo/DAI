import PizzaService from "./src/services/pizzas-services.js";

let srv = new PizzaService();
let data = await  srv.getAll();
console.log(data);