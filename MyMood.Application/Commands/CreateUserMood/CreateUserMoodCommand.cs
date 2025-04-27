using MediatR;

namespace MyMood.Application.Commands.CreateUserMood;

public class CreateUserMoodCommand : IRequest<CreateUserMoodCommandResponse>
{
    public required int UserId { get; set; }
    public required int MoodId { get; set; }
    public string? Comment { get; set; }
}
