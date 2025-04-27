using MediatR;
using MyMood.Infrastructure;
using MyMood.Infrastructure.Dtos;

namespace MyMood.Application.Commands.CreateUserMood;

public class CreateUserMoodCommandHandler
    : IRequestHandler<CreateUserMoodCommand, CreateUserMoodCommandResponse>
{
    private readonly IUserMoodsRepository _repository;

    public CreateUserMoodCommandHandler(IUserMoodsRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateUserMoodCommandResponse> Handle(
        CreateUserMoodCommand request,
        CancellationToken cancellationToken
    )
    {
        var currentDateTime = DateTime.UtcNow;
        var currentDay = DateOnly.FromDateTime(currentDateTime);
        var currentTime = TimeOnly.FromDateTime(DateTime.UtcNow);

        var exists = await _repository.UserEntryForDateExistsAsync(request.UserId, currentDay);
        if (exists)
        {
            return new CreateUserMoodCommandResponse()
            {
                IsSuccess = false,
                ErrorMessage = "Mood entry for today already exists.",
            };
        }

        var mood = new UserMoodDto
        {
            UserId = request.UserId,
            MoodId = request.MoodId,
            Comment = request.Comment,
            MoodDate = currentDateTime,
        };

        var newId = await _repository.AddUserMoodsRepositoryAsync(mood);

        return new CreateUserMoodCommandResponse() { IsSuccess = true, Id = newId };
    }
}
