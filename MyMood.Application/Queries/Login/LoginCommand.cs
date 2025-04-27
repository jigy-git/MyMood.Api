using MediatR;

namespace MyMood.Application.Queries.Login;

public class LoginCommand : IRequest<LoginCommandResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
