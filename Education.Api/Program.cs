using Education.Application.Cursos;
using Education.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EducationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCursoQuery.GetCursoQueryHandler).Assembly));

// Updated to use the recommended method for registering validators
builder.Services.AddValidatorsFromAssemblyContaining<GetCursoQuery>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; // Optional: Disable default DataAnnotations validation
});

builder.Services.AddAutoMapper(builder =>
{
    builder.AddMaps(typeof(GetCursoQuery));
}, typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
