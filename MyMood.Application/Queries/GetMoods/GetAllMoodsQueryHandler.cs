using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MyMood.Application.ViewModels;
using MyMood.Infrastructure;

namespace MyMood.Application.Queries.GetMoods;

public class GetAllMoodsQueryHandler : IRequestHandler<GetAllMoodsQuery, IEnumerable<Mood>>
{
    private readonly IMoodsRepository _moodRepository;
    private readonly IMemoryCache _cache;

    public GetAllMoodsQueryHandler(IMoodsRepository moodRepository, IMemoryCache cache)
    {
        _moodRepository = moodRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<Mood>> Handle(
        GetAllMoodsQuery request,
        CancellationToken cancellationToken
    )
    {
        if (
            request.UseCache
            && _cache.TryGetValue(
                ApplicationContants.CacheKeyForMoods,
                out IEnumerable<Mood>? cachedMoods
            )
            && cachedMoods?.Count() > 0
        )
        {
            return cachedMoods;
        }

        var moods = await _moodRepository.GetAllMoodsAsync();

        var moodList = moods.Select(m => new Mood(m.Id, m.Mood, m.Notes));
        _cache.Set(
            ApplicationContants.CacheKeyForMoods,
            moodList,
            TimeSpan.FromMinutes(ApplicationContants.CacheTimespanForMoodsInMinutes)
        );
        return moodList;
    }
}
