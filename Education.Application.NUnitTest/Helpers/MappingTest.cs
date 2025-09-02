using AutoMapper;
using Education.Application.DTOs;
using Education.Domain;

namespace Education.Application.Helpers;

public class MappingTest : Profile
{

    public MappingTest()
    {
        CreateMap<Curso, CursoDTO>();
    }


}
