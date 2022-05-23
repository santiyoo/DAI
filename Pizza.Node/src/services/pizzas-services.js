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
    getById = async () => {
    let returnEntity = null;
    console.log("Estoy en PizzaService.getById")
    try{
        let pool = await sql.connect(config);
        let result = await pool.request()
                                        .input('pId', sql.Int, id)
                                        .query('SELECT TOP 2 * FROM Pizzas WHERE id = @pId');
        returnEntity = result.recordsets[0][0]
    } 
    catch (error) {
        console.log(error)
    }
    return returnEntity;
} 


    deleteById = async (id) =>{

        let rowsAffected = 0;

        try {

            let pool = await sql.connect(config);
            let result = await pool.request()
                                            .input('pId', sql.Int, id)
                                            .query('DELETE FROM Pizzas WHERE id = @pId');
            rowsAffected = result.rowsAffected;

        } catch (error) {
            console.log(error)
        }
        return rowsAffected;
    } 
    
}

export default PizzaService;
