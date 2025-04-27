using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMood.Api.Requests;
using MyMood.Application.Commands.CreateUserMood;

namespace MyMood.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/usermood")]
public class UserMoodController : Controller
{
    private readonly IMediator _mediator;

    public UserMoodController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserMoodRequest request)
    {
        var id = await _mediator.Send(
            new CreateUserMoodCommand()
            {
                MoodId = request.MoodId,
                UserId = request.UserId,
                Comment = request.Comment,
            }
        );

        return CreatedAtAction(nameof(Create), new { id }, new { id });
    }
}
