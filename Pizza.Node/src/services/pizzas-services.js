import sql from 'mssql'
import config from '../../dbconfig.js'
import logger from '../modules/log-helper.js';

const hola = new logger();

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
            hola.logErr(error.toString())
        }
        return returnEntity;
    }

    getById = async (id) => {
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
            hola.logErr(error.toString())
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
            hola.logErr(error.toString())
        }
        return rowsAffected;
    }
    
    update = async (pizza) =>{
        let rowsAffected = 0;
        console.log('Estoy en PizzaService.update')
        try {

            let pool = await sql.connect(config);
            let result = await pool.request()
                                            .input('pId', sql.Int, pizza?.id?? 0)
                                            .input('pNombre', sql.NChar, pizza?.nombre?? '')
                                            .input('pLibreGluten', sql.Bit, pizza?.libreGluten?? false)
                                            .input('pImporte', sql.Float, pizza?.importe?? 0)
                                            .input('pDescripcion', sql.NChar, pizza?.descripcion ?? '')
                                            .query('UPDATE Pizzas SET Nombre = @pNombre, LibreGluten = @pLibreGluten, Importe = @pImporte, Descripcion = @pDescripcion WHERE Id = @pId');
            rowsAffected = result.rowsAffected;
        } catch (error) {
            hola.logErr(error.toString())
        }
        return rowsAffected;
    } 
    
    insert = async (pizza) =>{
        let rowsAffected = 0;
        console.log('Estoy en PizzaService.insert')
        try {

            let pool = await sql.connect(config);
            let result = await pool.request()
                                            .input('pNombre', sql.NChar, pizza.nombre)
                                            .input('pLibreGluten', sql.Bit, pizza.libreGluten)
                                            .input('pImporte', sql.Float, pizza.importe)
                                            .input('pDescripcion', sql.NChar, pizza.descripcion)
                                            .query('INSERT INTO Pizzas(Nombre, LibreGluten, Importe, Descripcion) VALUES (@pNombre, @pLibreGluten, @pImporte, @pDescripcion)');
            rowsAffected = result.rowsAffected;
        } catch (error) {
            hola.logErr(error.toString())
        }
        return rowsAffected;
    }
}

export default PizzaService;
