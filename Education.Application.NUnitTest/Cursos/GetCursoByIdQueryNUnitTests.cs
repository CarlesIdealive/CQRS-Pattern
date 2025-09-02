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
public class GetCursoByIdQueryNUnitTests
{
    private GetCursoByIdQuery.GetCursoByIdQueryHandler handlerCursoById;
    private Guid existingCursoId; // Para almacenar un ID existente

    [SetUp]
    public void Setup()
    {
        // Aquí puedes inicializar cualquier recurso necesario para las pruebas.
        // Inicializamos AutoFixture para la generacion automatica de datos
        existingCursoId = new Guid("05c2db79-e7b7-45fd-a0b0-b76a4309a792"); // Asegúrate de que este ID exista en los datos generados
        var fixture = new Fixture();
        var cursoRecords = fixture.CreateMany<Curso>(10).ToList();
        // Añadimos un curso específico con el ID existente
        cursoRecords.Add(fixture.Build<Curso>()
            .With(c => c.CursoId, existingCursoId)
            .Create());

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
        handlerCursoById = new GetCursoByIdQuery.GetCursoByIdQueryHandler(educationDbContextFake, mapper);


    }

    [Test]
    public async Task GetCursoByIdQueryHandler_InputCursoId_ReturnsNotNull()
    {
        // 2.Act
        GetCursoByIdQuery.GetCursoByIdQueryRequest request = new()
        {
            CursoId = existingCursoId // Usamos el ID existente
        };
        var result = await handlerCursoById.Handle(request, new CancellationToken());

        // 3.Assert
        Assert.That(result, Is.Not.Null);

    }






}
