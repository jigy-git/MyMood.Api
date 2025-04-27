using MediatR;
using MyMood.Domain;
using MyMood.Infrastructure;

namespace MyMood.Application.Queries.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IUsersRepository _usersRepository;

    public LoginCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<LoginCommandResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _usersRepository.GetUserRecordAsync(request.Email);
        if (user == null)
        {
            return new LoginCommandResponse() { IsAuthenticated = false };
        }

        var passwordVerifies = PasswordService.Verify(request.Password, user.PasswordHash);
        return passwordVerifies
            ? new LoginCommandResponse()
            {
                IsAuthenticated = true,
                UserId = user.Id,
                UserRole = user.UserRole,
            }
            : new LoginCommandResponse() { IsAuthenticated = false };
    }
}
