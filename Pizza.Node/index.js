import sql from 'mssql';
import config from "./dbconfig.js"
import PizzaService from "./src/services/pizzas-services.js"

// let pool = await sql.connect(config);
// let result = await pool.request().query("SELECT TOP 2 * FROM Pizzas");

// console.log(result.recordsets.length);

let data;

let srv = new PizzaService
data = await srv.getById(2);
console.log(data)