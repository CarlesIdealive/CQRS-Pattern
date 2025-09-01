using Education.Domain;
using Microsoft.EntityFrameworkCore;

namespace Education.Persistence;

public class EducationDbContext : DbContext
{
    public EducationDbContext()
    {
    }


    public EducationDbContext(DbContextOptions<EducationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Curso> Cursos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Curso>()
            .Property(c => c.Precio)
            .HasPrecision(14, 2);


        //modelBuilder.Entity<Curso>().HasData(
        //    new Curso
        //    {
        //        CursoId = new Guid("05c2db79-e7b7-45fd-a0b0-b76a4309a792"),
        //        Titulo = "Introducción a C#",
        //        Descripcion = "Aprende los conceptos básicos de C# y la programación orientada a objetos.",
        //        FechaPublicacion = DateTime.Now.AddDays(30),
        //        FechaCreacion = DateTime.Now,
        //        Precio = 49.99m,
        //        Duracion = 40
        //    },
        //    new Curso
        //    {
        //        CursoId = new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"),
        //        Titulo = "Desarrollo Web con ASP.NET Core",
        //        Descripcion = "Crea aplicaciones web modernas utilizando ASP.NET Core y Entity Framework Core.",
        //        FechaPublicacion = DateTime.Now.AddDays(45),
        //        FechaCreacion = DateTime.Now,
        //        Precio = 79.99m,
        //        Duracion = 60
        //    },
        //    new Curso
        //    {
        //        CursoId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //        Titulo = "Bases de Datos con SQL Server",
        //        Descripcion = "Aprende a diseñar, implementar y gestionar bases de datos relacionales con SQL Server.",
        //        FechaPublicacion = DateTime.Now.AddDays(60),
        //        FechaCreacion = DateTime.Now,
        //        Precio = 59.99m,
        //        Duracion = 50
        //    }
        //);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-VE4LCTO;Database=Education;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;");
        }
    }

}
