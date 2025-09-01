using AutoMapper;
using Education.Application.DTOs;
using Education.Domain;

namespace Education.Application.Mapping;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Curso, CursoDTO>();

    }




}
