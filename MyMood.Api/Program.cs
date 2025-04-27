using MyMood.Application;
using MyMood.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Missing database connection string");

builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CQRSRegistrationHandlerClass>()
);

builder.Services.AddSingleton<IMoodsRepository>(provider => new MoodsRepository(connectionString));
builder.Services.AddSingleton<IUserMoodsRepository>(provider => new UserMoodsRepository(
    connectionString
));
builder.Services.AddSingleton<IUsersRepository>(provider => new UsersRepository(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReactApp",
        policy =>
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowReactApp");
app.MapControllers();

app.Run();
