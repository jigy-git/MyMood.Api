using Dapper;
using MyMood.Infrastructure.Dtos;

namespace MyMood.Infrastructure;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(string connectionString)
        : base(connectionString) { }

    #region Queries
    public async Task<UserDto?> GetUserRecordAsync(string email)
    {
        var sql = @"SELECT Id, Email, PasswordHash, UserRole FROM Users WHERE Email = @Email";

        using var connection = GetConnection();
        var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { Email = email });
        return user;
    }
    #endregion Queries

    #region Commands
    public async Task<int> CreateUserRecordAsync(string email, string passwordHash, string userRole)
    {
        var sql =
            @"
            INSERT INTO Users (Email, PasswordHash, UserRole)
            VALUES (@Email, @PasswordHash, @UserRole);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        var parameters = new
        {
            Email = email,
            PasswordHash = passwordHash,
            UserRole = userRole,
        };

        using var connection = GetConnection();
        var userId = await connection.ExecuteScalarAsync<int>(sql, parameters);
        return userId;
    }

    #endregion Commands
}
