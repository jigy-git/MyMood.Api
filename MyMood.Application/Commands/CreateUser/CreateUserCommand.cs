using MediatR;

namespace MyMood.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreateUserCommandResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string UserRole { get; set; }
}
