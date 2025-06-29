using Lab.Application.UseCases.UserRoles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController(IMediator _mediator) : ControllerBase
{
    [HttpPost("assign")]
    public async Task<IActionResult> Assign([FromBody] AssignRoleToUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }
}