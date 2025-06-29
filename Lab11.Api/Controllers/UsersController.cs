using Lab.Application.UseCases.Users.Commands;
using Lab.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { token });
    }
}