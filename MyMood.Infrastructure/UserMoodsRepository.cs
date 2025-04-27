using Dapper;
using MyMood.Infrastructure.Dtos;

namespace MyMood.Infrastructure;

public class UserMoodsRepository : BaseRepository, IUserMoodsRepository
{
    public UserMoodsRepository(string connectionString)
        : base(connectionString) { }

    #region Queries
    public async Task<bool> UserEntryForDateExistsAsync(int userId, DateOnly moodDate)
    {
        var sql =
            @"SELECT COUNT(1)
                FROM UserMoods
                WHERE UserId = @UserId AND MoodDate = @MoodDate";

        using var connection = GetConnection();
        var count = await connection.ExecuteScalarAsync<int>(
            sql,
            new { UserId = userId, MoodDate = moodDate.ToString("yyyy-MM-dd") }
        );

        return count > 0;
    }

    public async Task<IEnumerable<UserMoodDto>> GetAllMoodsRegisteredBetweenDatesAsync(
        DateTime dateFrom,
        DateTime dateTo
    )
    {
        const string sql =
            @"
        SELECT *
        FROM UserMoods
        WHERE MoodDate BETWEEN @From AND @To;";

        using var connection = GetConnection();

        return await connection.QueryAsync<UserMoodDto>(
            sql,
            new { From = dateFrom.Date, To = dateTo.Date }
        );
    }

    #endregion Queries

    #region Commands
    public async Task<int> AddUserMoodsRepositoryAsync(UserMoodDto userMood)
    {
        var sql =
            @"INSERT INTO UserMoods (MoodDate, MoodId, UserId, MoodTime, Comment)
                    VALUES (@MoodDate, @MoodId, @UserId, @MoodTime, @Comment);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

        using var connection = GetConnection();
        var id = await connection.ExecuteScalarAsync<int>(
            sql,
            new
            {
                userMood.UserId,
                userMood.MoodId,
                userMood.MoodDate,
                userMood.MoodTime,
                userMood.Comment,
            }
        );

        return id;
    }

    #endregion Commands
}
