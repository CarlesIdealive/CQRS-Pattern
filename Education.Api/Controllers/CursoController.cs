using Education.Application.Cursos;
using Education.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{

    private IMediator _mediator;

    public CursoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CursoDTO>>> Get()
    {
        return await _mediator.Send(new GetCursoQuery.GetCursoQueryRequest());
    }

    [HttpPost]
    public async Task Crear(CreateCursoCommand.CreateCursoCommandRequest request)
    {
        await _mediator.Send(request);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<CursoDTO>>GetById(Guid Id)
    {
        var request = new GetCursoByIdQuery.GetCursoByIdQueryRequest { CursoId = Id };
        var result = await _mediator.Send(request);
        return Ok(result);
    }




}
