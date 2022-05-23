import sql from 'mssql'
import config from '../../dbconfig'


class PizzaService {

    getById = async (id) =>{

        let returnEntity = null;

        try {

            let pool = await sql.connect(config);
            let result = await pool.request()
                                            .input('pId', sql.Int, id)
                                            .query('SELECT TOP 2 * FROM Pizzas WHERE id = @pId');
            returnEntity = result.recordsets[0][0]

        } catch (error) {
            console.log(error)
        }
        returnEntity
    } 
}