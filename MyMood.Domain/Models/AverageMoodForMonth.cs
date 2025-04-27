namespace MyMood.Domain.Models;

public class AverageMoodForMonth
{
    public int MoodId { get; set; }
    public int Month { get; set; } // 1 = January, 12 = December
    public int Year { get; set; }
}
