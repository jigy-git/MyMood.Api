using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace MyMood.Infrastructure;

public class BaseRepository
{
    private readonly string _connectionString;

    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected DbConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
