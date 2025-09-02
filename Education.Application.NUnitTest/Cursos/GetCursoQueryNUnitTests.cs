using AutoFixture;
using AutoMapper;
using Education.Application.Helpers;
using Education.Domain;
using Education.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Education.Application.Cursos;

[TestFixture]
public class GetCursoQueryNUnitTests
{
    private GetCursoQuery.GetCursoQueryHandler handlerAllCursos;

    [SetUp]
    public void Setup()
    {
        // Aquí puedes inicializar cualquier recurso necesario para las pruebas.
        // Inicializamos AutoFixture para la generacion automatica de datos
        var fixture = new Fixture();
        var cursoRecords = fixture.CreateMany<Curso>(10).ToList();
        //Creamos BAses de Datos en Memoria
        var options = new DbContextOptionsBuilder<EducationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDatabase-{Guid.NewGuid()}")
            .Options;
        var educationDbContextFake = new EducationDbContext(options);
        //Agregamos ls entidades generadas automaticamente a la base de datos en memoria
        educationDbContextFake.Cursos.AddRange(cursoRecords);
        educationDbContextFake.SaveChanges();

        var loggerFactory = LoggerFactory.Create(builder => { /* No config */ });
        var mapConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingTest>();
        }, loggerFactory: loggerFactory);
        var mapper = mapConfig.CreateMapper();

        // Finalmente creamos el handler
        handlerAllCursos = new GetCursoQuery.GetCursoQueryHandler(educationDbContextFake, mapper);


    }

    [Test]
    public async Task GetCursoQueryHandler_QueryCursos_ReturnsTrueAsync()
    {
        // 1.Arrange
        // Aquí deberías configurar un contexto de base de datos en memoria y un mapper simulado.
        //Lo hacemos en el Setup
        //Instanciamos el Handler
        // Lo hacemos en el Setup


        // 2.Act
        GetCursoQuery.GetCursoQueryRequest request = new();
        var result = await handlerAllCursos.Handle(request, new CancellationToken());

        // 3.Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);

    }





}
