using Lab.Application.UseCases.Tickets.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }
}