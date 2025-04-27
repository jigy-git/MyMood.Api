using System.Data;
using Microsoft.Data.SqlClient;
using MyMood.Infrastructure;

namespace MyMood.Api;

public static class ServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddScoped<IDbConnection>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        });

        services.AddSingleton<IUserMoodsRepository, UserMoodsRepository>();
        services.AddSingleton<IMoodsRepository, MoodsRepository>();
    }
}
