using Dapper;
using MyMood.Infrastructure.Dtos;

namespace MyMood.Infrastructure;

public class MoodsRepository : BaseRepository, IMoodsRepository
{
    public MoodsRepository(string connectionString)
        : base(connectionString) { }

    public async Task<IEnumerable<MoodDto>> GetAllMoodsAsync()
    {
        using var connection = GetConnection();
        string query = "SELECT * FROM Moods";
        var records = await connection.QueryAsync<MoodDto>(query);
        return records;
    }
}
