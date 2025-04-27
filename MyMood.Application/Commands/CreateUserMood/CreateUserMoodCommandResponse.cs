namespace MyMood.Application.Commands.CreateUserMood;

public class CreateUserMoodCommandResponse
{
    public bool IsSuccess { get; set; }
    public int Id { get; set; } = -1;
    public string? ErrorMessage { get; set; } = string.Empty;
}
