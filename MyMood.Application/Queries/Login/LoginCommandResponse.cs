namespace MyMood.Application.Queries.Login;

public class LoginCommandResponse
{
    public bool IsAuthenticated { get; set; }
    public int? UserId { get; set; }
    public string UserRole { get; set; } = string.Empty;
}
