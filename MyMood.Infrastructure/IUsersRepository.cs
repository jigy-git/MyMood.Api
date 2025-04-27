using MyMood.Infrastructure.Dtos;

namespace MyMood.Infrastructure;

public interface IUsersRepository
{
    public Task<UserDto?> GetUserRecordAsync(string user);

    public Task<int> CreateUserRecordAsync(string user, string passwordHash, string userRole);
}
