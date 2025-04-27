namespace MyMood.Application.ViewModels;

public record WeeklyAverageMood(
    int MoodId,
    string Mood,
    DateOnly StartDayOfWeek,
    DateOnly LastDayOfWeek
);
