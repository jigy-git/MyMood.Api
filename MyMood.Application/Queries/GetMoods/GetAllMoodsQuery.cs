using MediatR;
using MyMood.Application.ViewModels;

namespace MyMood.Application.Queries.GetMoods;

public class GetAllMoodsQuery : IRequest<IEnumerable<Mood>>
{
    public bool UseCache { get; set; } = true;
}
