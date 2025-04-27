using MediatR;
using MyMood.Domain;
using MyMood.Infrastructure;

namespace MyMood.Application.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly IUsersRepository _repository;

    public CreateUserCommandHandler(IUsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateUserCommandResponse> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _repository.GetUserRecordAsync(request.Email);
        if (user != null)
        {
            return new CreateUserCommandResponse()
            {
                IsSuccess = false,
                ErrorMessage = "Mood entry for today already exists.",
            };
        }

        var pwdHash = PasswordService.Hash(request.Password);
        var newId = await _repository.CreateUserRecordAsync(
            request.Email,
            pwdHash,
            request.UserRole
        );

        return new CreateUserCommandResponse() { IsSuccess = true, Id = newId };
    }
}
