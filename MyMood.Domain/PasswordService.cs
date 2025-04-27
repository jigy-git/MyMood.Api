namespace MyMood.Domain;

using BCrypt.Net;

public static class PasswordService
{
    public static string Hash(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public static bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Verify(password, hashedPassword);
    }
}
