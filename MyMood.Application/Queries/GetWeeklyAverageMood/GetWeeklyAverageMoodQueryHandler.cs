using MediatR;
using MyMood.Application.Queries.GetMoods;
using MyMood.Application.ViewModels;
using MyMood.Domain;
using MyMood.Domain.Utils;
using MyMood.Infrastructure;

namespace MyMood.Application.Queries.GetWeeklyAverageMood;

public class GetWeeklyAverageMoodQueryHandler
    : IRequestHandler<GetWeeklyAverageMoodQuery, IEnumerable<WeeklyAverageMood>>
{
    private readonly IUserMoodsRepository _userMoodsRepository;
    private readonly IMediator _mediator;

    public GetWeeklyAverageMoodQueryHandler(
        IUserMoodsRepository userMoodsRepository,
        IMediator mediator
    )
    {
        _userMoodsRepository = userMoodsRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<WeeklyAverageMood>> Handle(
        GetWeeklyAverageMoodQuery request,
        CancellationToken cancellationToken
    )
    {
        var moods = await _mediator.Send(
            new GetAllMoodsQuery() { UseCache = true },
            cancellationToken
        );

        var userMoods = await _userMoodsRepository.GetAllMoodsRegisteredBetweenDatesAsync(
            request.FromDate.ToUniversalTime(),
            request.ToDate.ToUniversalTime()
        );

        var weeklyAverageMoodIds = MoodAnalyticsService.CalculateWeeklyAverageMoodRating(
            DateOnly.FromDateTime(request.FromDate),
            userMoods.Select(m => new Domain.Models.UserDailyMood()
            {
                MoodId = m.MoodId,
                UserId = m.UserId,
                Date = m.MoodDate.ConvertUtcToLocalDateOnly(),
            })
        );

        var weeklyAverageMoods = weeklyAverageMoodIds.Join(
            moods,
            avg => avg.MoodId,
            mood => mood.Id,
            (avg, mood) =>
                new WeeklyAverageMood(mood.Id, mood.MoodName, avg.WeekStartDate, avg.WeekEndDate)
        );

        return weeklyAverageMoods;
    }
}
