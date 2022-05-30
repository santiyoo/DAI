import { Router } from "express";
import PizzaService from "../services/pizzas-services";

const router = Router()
const pizzaService = new PizzaService();

router.get('', async (req, res)=>{
    const pizza = await pizzaService.getAll();
    return res.status(200).json
})