namespace MyMood.Infrastructure.Dtos;

public class MoodDto
{
    public int Id { get; set; }
    public string Mood { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
