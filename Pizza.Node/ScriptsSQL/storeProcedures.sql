CREATE PROCEDURE traerPizza
@id int
AS
BEGIN
	SELECT * From Pizzas WHERE id = @id
END

CREATE PROCEDURE agregarPizza
@nombre varchar(150),
@libreGluten bit,
@importe float,
@descripcion varchar(MAX)
AS
BEGIN
	INSERT INTO Pizzas(Nombre, LibreGluten, Importe, Descripcion) 
	VALUES (@nombre,@libreGluten, @importe, @descripcion)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END

CREATE PROCEDURE actualizarPizza
@id int,
@nombre varchar(150),
@libreGluten bit,
@importe float,
@descripcion varchar(MAX)
AS
BEGIN
	UPDATE Pizzas SET Nombre = @nombre, LibreGluten = @libreGluten, Importe = @importe, Descripcion = @descripcion WHERE Id = @id
END

CREATE PROCEDURE eliminarPizza
@id int
AS
BEGIN
	DELETE FROM Pizzas WHERE id=@id
END