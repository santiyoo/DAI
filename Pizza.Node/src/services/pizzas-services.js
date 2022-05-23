import sql from 'mssql'
import config from '../../dbconfig.js'


class PizzaService {
    getAll = async ()=>{
        let returnEntity = null;
        console.log("Estoy en PizzaService.getAll")
        try{
            let pool = await sql.connect(config);
            let result = await pool.request().query("SELECT * FROM Pizzas");
            returnEntity = result.recordset;
        }
        catch(error){
            console.log(error);
        }
        return returnEntity;
    }
}

export default PizzaService;