using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMood.Api.Requests;
using MyMood.Application.Commands.CreateUser;
using MyMood.Application.Queries.Login;

namespace MyMood.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _mediator.Send(new LoginCommand(request.Email, request.Password));
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid email or password.");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var userId = await _mediator.Send(
            new CreateUserCommand()
            {
                Email = request.Email,
                Password = request.Password,
                UserRole = request.UserRole,
            }
        );
        return CreatedAtAction(nameof(Register), new { id = userId }, new { UserId = userId });
    }
}
