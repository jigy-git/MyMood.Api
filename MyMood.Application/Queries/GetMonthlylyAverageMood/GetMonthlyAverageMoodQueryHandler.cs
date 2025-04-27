using MediatR;
using MyMood.Application.Queries.GetMoods;
using MyMood.Application.ViewModels;
using MyMood.Domain;
using MyMood.Domain.Utils;
using MyMood.Infrastructure;

namespace MyMood.Application.Queries.GetMonthlyAverageMood;

public class GetMonthlyAverageMoodQueryHandler
    : IRequestHandler<GetMonthlyAverageMoodQuery, IEnumerable<MonthlyAverageMood>>
{
    private readonly IUserMoodsRepository _userMoodsRepository;
    private readonly IMediator _mediator;

    public GetMonthlyAverageMoodQueryHandler(
        IUserMoodsRepository userMoodsRepository,
        IMediator mediator
    )
    {
        _userMoodsRepository = userMoodsRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<MonthlyAverageMood>> Handle(
        GetMonthlyAverageMoodQuery request,
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

        var monthlyAverageMoodIds = MoodAnalyticsService.CalculateMonthlyAverageMoodRating(
            userMoods.Select(m => new Domain.Models.UserDailyMood()
            {
                MoodId = m.MoodId,
                UserId = m.UserId,
                Date = m.MoodDate.ConvertUtcToLocalDateOnly(),
            })
        );

        var monthlyAverageMoods = monthlyAverageMoodIds.Join(
            moods,
            avg => avg.MoodId,
            mood => mood.Id,
            (avg, mood) => new MonthlyAverageMood(mood.Id, mood.MoodName, avg.Month, avg.Year)
        );

        return monthlyAverageMoods;
    }
}
