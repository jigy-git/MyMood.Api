namespace MyMood.Infrastructure.Dtos;

public class UserMoodDto
{
    public required int MoodId { get; set; }
    public DateTime MoodDate { get; set; }
    public TimeSpan MoodTime { get; set; }
    public required int UserId { get; set; }
    public string? Comment { get; set; }
}
