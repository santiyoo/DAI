import sql from 'mssql'
import config from '../../dbconfig'


class PizzaService {

    getById = async (id) =>{

        let returnEntity = null;

        try {

            let pool = await sql.connect(config);
            let result = await pool.request()
                                            .input(pid)
                                            .query("SELECT TOP 2 * FROM Pizzas");

        } catch (error) {
            
        }
    } 
}