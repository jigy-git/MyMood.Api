using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMood.Application.Queries.GetMoods;
using MyMood.Application.ViewModels;

namespace MyMood.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/moods")]
public class MoodsController : Controller
{
    private readonly IMediator _mediator;

    public MoodsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Mood>>> Index()
    {
        var moods = await _mediator.Send(new GetAllMoodsQuery());
        return Ok(moods);
    }
}
