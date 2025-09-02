using MediatR;
using FluentValidation;
using Education.Domain;
using Education.Persistence;

namespace Education.Application.Cursos;

public class CreateCursoCommand
{
    public class CreateCursoCommandRequest : IRequest    
    {   
        //public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public decimal Precio { get; set; }
        public int Duracion { get; set; }
    }

    public class CreateCursoCommandRequestValidation : AbstractValidator<CreateCursoCommandRequest>
    {
        public CreateCursoCommandRequestValidation()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres.");
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.");
            RuleFor(x => x.FechaPublicacion)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de publicación debe ser una fecha futura.");
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");
            RuleFor(x => x.Duracion)
                .GreaterThan(0).WithMessage("La duración debe ser mayor que cero.");
        }
    }

    public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest>
    {
        private readonly EducationDbContext _context;
        public CreateCursoCommandHandler(EducationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
        {
            var curso = new Curso
            {
                CursoId = Guid.NewGuid(),
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                FechaCreacion = DateTime.UtcNow,
                FechaPublicacion = request.FechaPublicacion,
                Precio = request.Precio,
                Duracion = request.Duracion
            };
            _context.Cursos.Add(curso);
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                throw new Exception("No se pudo crear el curso");
            }

            ////Ahora lo busca en la base de datos para verificar que se creo
            //var cursoCreado = await _context.Cursos.FindAsync(curso.CursoId);
            //if (cursoCreado == null)
            //{
            //    throw new Exception("No se pudo encontrar el curso creado");
            //}

            return;
        }


    }



}
