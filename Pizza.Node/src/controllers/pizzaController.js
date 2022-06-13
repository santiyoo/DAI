import { Router } from "express";
import PizzaService from "../services/pizzas-services.js";

const router = Router()
const srv = new PizzaService();

// router.get('', async (req, res)=>{
//     const pizza = await pizzaService.getAll();
//     return res.status(200).json
// })

router.get('/', async (req, res) => {
    console.log('router.get');
    let datos = await srv.getAll();
    res.send(datos);
  })

  router.get('/:id', async (req, res) => {
    console.log('router.get');
    let datos = await srv.getById(req.params['id']);

    if (datos != null) {
      res.send(datos);
    } else {
      res.status(404).send('Perdon, no se ha encontrado');
    }

  })

  router.delete('/:id', async (req, res) => {
    console.log('router.delete');
    let datos = await srv.deleteById(req.params['id']);
    if (datos != 0) {
      res.send(datos);
    } else {
      res.status(404).send('Esa pizza no existe :)');
    }
  })

  router.put('', async (req, res) => {
    console.log('router.put');
    let datos = await srv.update(req.body);
    if (datos != 0) {
      res.send(datos);
    } else {
      res.status(404).send('Error');
    }
  })

  router.post('', async (req, res) => {
    console.log('router.post');
    let datos = await srv.insert(req.body);
    if (datos != null) {
      res.send(datos);
    } else {
      res.status(404).send('Error');
    }
  })
/*
router.get('/api/pizzas', srv.getAll)
router.get('/api/pizzas/:Id', srv.getById)
router.post('/api/pizzas', srv.insert)
router.put('/api/pizzas', srv.update)
router.delete('/api/pizzas/:Id', srv.deleteById)
*/
export default router;