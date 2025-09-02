using AutoFixture;
using Education.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Education.Application.Cursos;

[TestFixture]
public class CreateCursoCommandNUnitTests
{
    private CreateCursoCommand.CreateCursoCommandHandler handlerCursoCreate;
    private EducationDbContext educationDbContextFake; // <-- Añade esto

    [SetUp]
    public void Setup()
    {
        // Aquí puedes inicializar cualquier recurso necesario para las pruebas.
        // Inicializamos AutoFixture para la generacion automatica de datos
        //Creamos BAses de Datos en Memoria
        var options = new DbContextOptionsBuilder<EducationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDatabase-{Guid.NewGuid()}")
            .Options;
        educationDbContextFake = new EducationDbContext(options);
        //Agregamos ls entidades generadas automaticamente a la base de datos en memoria
        // Finalmente creamos el handler
        handlerCursoCreate = new CreateCursoCommand.CreateCursoCommandHandler(educationDbContextFake);
    }



    [Test]
    public async Task CreateCursoCommand_InputCurso_ReturnsTrueAsync()
    {
        // 1.Arrange
        // Aquí deberías configurar un contexto de base de datos en memoria y un mapper simulado.
        //Lo hacemos en el Setup
        //Instanciamos el Handler
        // Lo hacemos en el Setup
        CreateCursoCommand.CreateCursoCommandRequest cursoRecordManual = new()
        {
            Titulo = "Curso de Prueba",
            Descripcion = "Descripción del curso de prueba",
            FechaPublicacion = DateTime.UtcNow.AddDays(10), // Fecha futura
            Precio = 99.99m,
            Duracion = 20
        };

        // 2.Act
        await handlerCursoCreate.Handle(cursoRecordManual, new CancellationToken());

        // 3.Assert
        var cursoEnDb = await educationDbContextFake.Cursos
           .FirstOrDefaultAsync(c => c.Titulo == cursoRecordManual.Titulo && c.Descripcion == cursoRecordManual.Descripcion);

        Assert.That(cursoEnDb, Is.Not.Null);
        Assert.That(cursoEnDb.Titulo, Is.EqualTo(cursoRecordManual.Titulo));
        Assert.That(cursoEnDb.Descripcion, Is.EqualTo(cursoRecordManual.Descripcion));
        Assert.That(cursoEnDb.FechaPublicacion, Is.EqualTo(cursoRecordManual.FechaPublicacion));
        Assert.That(cursoEnDb.Precio, Is.EqualTo(cursoRecordManual.Precio));
        Assert.That(cursoEnDb.Duracion, Is.EqualTo(cursoRecordManual.Duracion));
    }
}