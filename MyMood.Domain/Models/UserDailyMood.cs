namespace MyMood.Domain.Models;

public class UserDailyMood
{
    public int MoodId { get; set; }
    public DateOnly Date { get; set; }
    public int UserId { get; set; }
}
