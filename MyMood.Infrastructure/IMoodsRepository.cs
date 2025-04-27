using MyMood.Infrastructure.Dtos;

namespace MyMood.Infrastructure;

public interface IMoodsRepository
{
    public Task<IEnumerable<MoodDto>> GetAllMoodsAsync();
}
