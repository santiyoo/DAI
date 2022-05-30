import express from "express";

const app = express()
const port = 3000;

app.get('/api/pizzas', (req, res) => {
    res.send("Hello World!")
})

app.get('/api/pizzas/:id', (req, res) => {
    res.send("Hello World!")
})

app.post('/api/pizzas', (req, res) => {
    res.send(req.body)
})

app.get('/polshu', (req, res) => {
    res.statusCode(200).send("Hello Polshu!")
})

app.get('/test', (req, res) => {
    res.send(req.query)
})

app.get('/test/:id', (req, res) => {
    res.send(req.params.id)
})

// app.use(function(req, res, next)){
//     console.log('Time: ', Date.now())
//     next();
// }

app.listen(port, ()=>{
    console.log(`Escuchando el Servido en el puerto: ${port}`)
})