using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using Education.Persistence;
using Education.Application.DTOs;

namespace Education.Application.Cursos;

public class GetCursoQuery
{
    //Representa los parametros de entrada (que manda el Cliente) en este caso ninguno {void}
    public class GetCursoQueryRequest : IRequest<List<CursoDTO>> { }


    //Handler (Manejador) que contiene la logica de negocio
    public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<CursoDTO>>
    {
        private readonly EducationDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoQueryHandler(EducationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<CursoDTO>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
        {
            var cursos = await _context.Cursos.ToListAsync(cancellationToken);
            var cursosDTO = _mapper.Map<List<CursoDTO>>(cursos);
            return cursosDTO;
        }




    }







}
