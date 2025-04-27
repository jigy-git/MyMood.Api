using MediatR;
using MyMood.Application.ViewModels;

namespace MyMood.Application.Queries.GetDailyAverageMood;

public class GetDailyAverageMoodQuery : IRequest<IEnumerable<DailyAverageMood>>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
