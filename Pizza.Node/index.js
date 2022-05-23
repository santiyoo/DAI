import sql from 'mssql';
import config from "./dbconfig.js"

let pool = await sql.connect(config);
let result = await pool.request().query("SELECT TOP 2 * FROM Pizzas");

console.log(result.recordsets.length);