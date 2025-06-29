using Lab.Application.UseCases.Responses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponsesController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    //[Authorize(Roles = "Admin")] // opcional
    public async Task<IActionResult> Create([FromBody] CreateResponseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }
}