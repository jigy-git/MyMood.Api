using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMood.Api.Requests;
using MyMood.Application.Queries.GetDailyAverageMood;
using MyMood.Application.Queries.GetMonthlyAverageMood;
using MyMood.Application.Queries.GetWeeklyAverageMood;
using MyMood.Application.ViewModels;

namespace MyMood.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/moodRating")]
public class MoodRatingController : Controller
{
    private readonly IMediator _mediator;

    public MoodRatingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("daily")]
    public async Task<ActionResult<IEnumerable<DailyAverageMood>>> GetDailyAverageRatings(
        GetDailyAverageRatingsRequest request
    )
    {
        var dailyAverageMoods = await _mediator.Send(
            new GetDailyAverageMoodQuery() { FromDate = request.FromDate, ToDate = request.ToDate }
        );

        return Ok(dailyAverageMoods);
    }

    [HttpPost("weekly")]
    public async Task<ActionResult<IEnumerable<WeeklyAverageMood>>> GetWeeklyAverageRatings(
        GetWeeklyAverageRatingsRequest request
    )
    {
        var moods = await _mediator.Send(
            new GetWeeklyAverageMoodQuery() { FromDate = request.FromDate, ToDate = request.ToDate }
        );

        return Ok(moods);
    }

    [HttpPost("monthly")]
    public async Task<ActionResult<IEnumerable<MonthlyAverageMood>>> GetMonthAverageRatings(
        GetMonthlyAverageRatingsRequest request
    )
    {
        var moods = await _mediator.Send(
            new GetMonthlyAverageMoodQuery()
            {
                FromDate = request.FromDate,
                ToDate = request.ToDate,
            }
        );

        return Ok(moods);
    }
}
