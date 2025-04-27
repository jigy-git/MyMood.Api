namespace MyMood.Application.Commands.CreateUser;

public class CreateUserCommandResponse
{
    public bool IsSuccess { get; set; }
    public int Id { get; set; } = -1;
    public string? ErrorMessage { get; set; } = string.Empty;
}
