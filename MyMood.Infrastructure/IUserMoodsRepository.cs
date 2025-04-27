using MyMood.Infrastructure.Dtos;

namespace MyMood.Infrastructure;

public interface IUserMoodsRepository
{
    public Task<bool> UserEntryForDateExistsAsync(int userId, DateOnly moodDate);
    public Task<IEnumerable<UserMoodDto>> GetAllMoodsRegisteredBetweenDatesAsync(
        DateTime dateFrom,
        DateTime dateTo
    );
    public Task<int> AddUserMoodsRepositoryAsync(UserMoodDto userMood);
}
