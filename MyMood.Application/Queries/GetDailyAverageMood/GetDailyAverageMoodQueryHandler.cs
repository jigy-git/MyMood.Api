using MediatR;
using MyMood.Application.Queries.GetMoods;
using MyMood.Application.ViewModels;
using MyMood.Domain;
using MyMood.Domain.Utils;
using MyMood.Infrastructure;

namespace MyMood.Application.Queries.GetDailyAverageMood;

public class GetDailyAverageMoodQueryHandler
    : IRequestHandler<GetDailyAverageMoodQuery, IEnumerable<DailyAverageMood>>
{
    private readonly IUserMoodsRepository _userMoodsRepository;
    private readonly IMediator _mediator;

    public GetDailyAverageMoodQueryHandler(
        IUserMoodsRepository userMoodsRepository,
        IMediator mediator
    )
    {
        _userMoodsRepository = userMoodsRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<DailyAverageMood>> Handle(
        GetDailyAverageMoodQuery request,
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

        var dailyAverageMoodIds = MoodAnalyticsService.CalculateDailyAverageMoodRating(
            userMoods.Select(m => new Domain.Models.UserDailyMood()
            {
                MoodId = m.MoodId,
                UserId = m.UserId,
                Date = m.MoodDate.ConvertUtcToLocalDateOnly(),
            })
        );

        var dailyAverageMoods = dailyAverageMoodIds.Join(
            moods,
            avg => avg.MoodId,
            mood => mood.Id,
            (avg, mood) => new DailyAverageMood(mood.Id, mood.MoodName, avg.Date)
        );

        return dailyAverageMoods;
    }
}
