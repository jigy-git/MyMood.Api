namespace MyMood.Api.Requests;

/// <summary>
/// Request to create User - needs email, password, and user role.
/// </summary>
public class CreateUserRequest
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string UserRole { get; set; } = string.Empty;
}
