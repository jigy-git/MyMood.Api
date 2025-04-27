namespace MyMood.Application.ViewModels;

public record UserMood(int Id, string UserId, string MoodId, string MoodName, string? Notes);
