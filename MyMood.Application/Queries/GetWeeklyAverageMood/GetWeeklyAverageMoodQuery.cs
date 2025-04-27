using MediatR;
using MyMood.Application.ViewModels;

namespace MyMood.Application.Queries.GetWeeklyAverageMood;

public class GetWeeklyAverageMoodQuery : IRequest<IEnumerable<WeeklyAverageMood>>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
