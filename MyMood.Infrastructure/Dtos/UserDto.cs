namespace MyMood.Infrastructure.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string UserRole { get; set; }
}
