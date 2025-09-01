using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Education.Application.DTOs;
using Education.Persistence;
using FluentValidation;
using MediatR;

namespace Education.Application.Cursos;

public class GetCursoByIdQuery
{
    public class GetCursoByIdQueryRequest : IRequest<CursoDTO>
    {
        //Representa los parametros de entrada (que manda el Cliente)
        public Guid CursoId { get; set; }
    }

    public class GetCursoByIdQueryRequestValidation : AbstractValidator<GetCursoByIdQueryRequest>
    {
        public GetCursoByIdQueryRequestValidation()
        {
            RuleFor(x => x.CursoId)
                .Must(x => x.GetType().Name.Equals("Guid")).WithMessage("El Id del curso debe ser un Guid.")
                .NotEmpty().WithMessage("El Id del curso es obligatorio.")
                .NotNull().WithMessage("El Id del curso no puede ser nulo.");
        }
    }

    public class GetCursoByIdQueryHandler : IRequestHandler<GetCursoByIdQueryRequest, CursoDTO>
    {
        private readonly EducationDbContext _context;
        private readonly IMapper _mapper;
        public GetCursoByIdQueryHandler(EducationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CursoDTO> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.CursoId == request.CursoId);
            if (curso == null)
            {
                throw new KeyNotFoundException($"No se encontró el curso con Id {request.CursoId}");
            }
            CursoDTO cursoDTO = _mapper.Map<CursoDTO>(curso);
            return cursoDTO;
        }
    }









}
