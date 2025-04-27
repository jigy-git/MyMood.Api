namespace MyMood.Domain.Models;

public class AverageMoodForWeek
{
    public int MoodId { get; set; }
    public DateOnly WeekStartDate { get; set; }
    public DateOnly WeekEndDate { get; set; }
}
