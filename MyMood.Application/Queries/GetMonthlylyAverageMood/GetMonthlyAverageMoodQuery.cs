using MediatR;
using MyMood.Application.ViewModels;

namespace MyMood.Application.Queries.GetMonthlyAverageMood;

public class GetMonthlyAverageMoodQuery : IRequest<IEnumerable<MonthlyAverageMood>>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
