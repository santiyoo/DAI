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
            console.log(error.toString());
            hola.logErr(error.toString());
        }
        return  returnEntity;
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
                                            .input('pId', sql.Int, pizza?.Id?? 0)
                                            .input('pNombre', sql.NChar, pizza?.Nombre?? '')
                                            .input('pLibreGluten', sql.Bit, pizza?.LibreGluten?? false)
                                            .input('pImporte', sql.Float, pizza?.Importe?? 0)
                                            .input('pDescripcion', sql.NChar, pizza?.Descripcion ?? '')
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
            console.log(pizza);
            let pool = await sql.connect(config);
            let result = await pool.request()
                                            .input('pNombre', sql.NChar, pizza.Nombre)
                                            .input('pLibreGluten', sql.Bit, pizza.LibreGluten)
                                            .input('pImporte', sql.Float, pizza.Importe)
                                            .input('pDescripcion', sql.NChar, pizza.Descripcion)
                                            .query('INSERT INTO Pizzas(Nombre, LibreGluten, Importe, Descripcion) VALUES (@pNombre, @pLibreGluten, @pImporte, @pDescripcion)');
            rowsAffected = result.rowsAffected;
        } catch (error) {
            hola.logErr(error.toString())
        }
        return rowsAffected;
    }
}

export default PizzaService;
