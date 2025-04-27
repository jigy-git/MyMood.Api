using MyMood.Domain.Models;

namespace MyMood.Tests.MoqData;

public static class TestDataSamples
{
    public static List<UserDailyMood> GetSampleData()
    {
        return new List<UserDailyMood>
        {
            new()
            {
                UserId = 1,
                Date = new DateOnly(2024, 01, 01),
                MoodId = 1,
            },
            new()
            {
                UserId = 2,
                Date = new DateOnly(2024, 01, 01),
                MoodId = 2,
            },
            new()
            {
                UserId = 3,
                Date = new DateOnly(2024, 01, 01),
                MoodId = 2,
            },
            new()
            {
                UserId = 4,
                Date = new DateOnly(2024, 01, 02),
                MoodId = 3,
            },
            new()
            {
                UserId = 5,
                Date = new DateOnly(2024, 01, 02),
                MoodId = 3,
            },
            new()
            {
                UserId = 6,
                Date = new DateOnly(2024, 01, 02),
                MoodId = 1,
            },
            new()
            {
                UserId = 7,
                Date = new DateOnly(2024, 01, 02),
                MoodId = 4,
            },
            new()
            {
                UserId = 8,
                Date = new DateOnly(2024, 01, 02),
                MoodId = 4,
            },
            new()
            {
                UserId = 7,
                Date = new DateOnly(2024, 01, 08),
                MoodId = 3,
            },
            new()
            {
                UserId = 1,
                Date = new DateOnly(2024, 01, 09),
                MoodId = 3,
            },
            new()
            {
                UserId = 2,
                Date = new DateOnly(2024, 01, 09),
                MoodId = 3,
            },
            new()
            {
                UserId = 8,
                Date = new DateOnly(2024, 02, 01),
                MoodId = 2,
            },
            new()
            {
                UserId = 9,
                Date = new DateOnly(2024, 02, 01),
                MoodId = 2,
            },
            new()
            {
                UserId = 10,
                Date = new DateOnly(2024, 02, 01),
                MoodId = 1,
            },
            new()
            {
                UserId = 1,
                Date = new DateOnly(2024, 03, 01),
                MoodId = 1,
            },
            new()
            {
                UserId = 2,
                Date = new DateOnly(2024, 03, 01),
                MoodId = 2,
            },
            new()
            {
                UserId = 3,
                Date = new DateOnly(2024, 03, 01),
                MoodId = 2,
            },
            new()
            {
                UserId = 4,
                Date = new DateOnly(2024, 03, 02),
                MoodId = 3,
            },
            new()
            {
                UserId = 5,
                Date = new DateOnly(2024, 03, 02),
                MoodId = 3,
            },
            new()
            {
                UserId = 6,
                Date = new DateOnly(2024, 03, 02),
                MoodId = 1,
            },
            new()
            {
                UserId = 7,
                Date = new DateOnly(2024, 03, 02),
                MoodId = 4,
            },
            new()
            {
                UserId = 8,
                Date = new DateOnly(2024, 03, 02),
                MoodId = 4,
            },
            new()
            {
                UserId = 7,
                Date = new DateOnly(2024, 03, 08),
                MoodId = 3,
            }, // new week
            new()
            {
                UserId = 1,
                Date = new DateOnly(2024, 03, 09),
                MoodId = 3,
            },
            new()
            {
                UserId = 2,
                Date = new DateOnly(2024, 03, 09),
                MoodId = 3,
            },
            new()
            {
                UserId = 1,
                Date = new DateOnly(2024, 03, 09),
                MoodId = 1,
            },
            new()
            {
                UserId = 2,
                Date = new DateOnly(2024, 03, 09),
                MoodId = 2,
            },
            new()
            {
                UserId = 3,
                Date = new DateOnly(2024, 03, 19),
                MoodId = 2,
            },
            new()
            {
                UserId = 4,
                Date = new DateOnly(2024, 03, 19),
                MoodId = 3,
            },
            new()
            {
                UserId = 5,
                Date = new DateOnly(2024, 03, 19),
                MoodId = 3,
            },
            new()
            {
                UserId = 6,
                Date = new DateOnly(2024, 03, 19),
                MoodId = 1,
            },
            new()
            {
                UserId = 7,
                Date = new DateOnly(2024, 03, 19),
                MoodId = 4,
            },
            new()
            {
                UserId = 8,
                Date = new DateOnly(2024, 03, 27),
                MoodId = 4,
            },
        };
    }
}
