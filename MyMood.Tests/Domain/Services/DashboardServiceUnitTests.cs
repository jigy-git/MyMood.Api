using MyMood.Domain;
using MyMood.Domain.Models;
using MyMood.Tests.MoqData;

namespace MyMood.Tests.Domain.Services;

public class DashboardServiceUnitTests
{
    [Fact]
    public void CalculateDailyAverageMoodRating_ReturnsCorrectResults()
    {
        var data = TestDataSamples.GetSampleData();
        var result = MoodAnalyticsService.CalculateDailyAverageMoodRating(data).ToList();

        // Assert that there is only 1 result per day
        var distinctDates = result.Select(r => r.Date).Distinct();
        Assert.Equal(distinctDates.Count(), result.Count); // no duplicate days

        // Day: 2024-01-01 → MoodId 2 appears twice
        var jan1 = result.FirstOrDefault(r => r.Date == new DateOnly(2024, 01, 01));
        Assert.NotNull(jan1);
        Assert.Equal(2, jan1.MoodId);

        // Day: 2024-01-02 → MoodId 3 and 4 both appear twice → tie breaker can go either way
        var jan2 = result.FirstOrDefault(r => r.Date == new DateOnly(2024, 01, 02));
        Assert.NotNull(jan2);
        Assert.Contains(jan2.MoodId, new[] { 3, 4 });

        // Day: 2024-03-19 → MoodId 3 appears twice
        var mar19 = result.FirstOrDefault(r => r.Date == new DateOnly(2024, 03, 19));
        Assert.NotNull(mar19);
        Assert.Equal(3, mar19.MoodId);
    }

    [Fact]
    public void CalculateWeeklyAverageMoodRating_ReturnsCorrectResults()
    {
        var data = TestDataSamples.GetSampleData();
        var result = MoodAnalyticsService.CalculateWeeklyAverageMoodRating(
            new DateOnly(2024, 01,01),
            data).ToList();

        // Assert that each WeekStartDate is unique
        var distinctWeeks = result.Select(r => r.WeekStartDate).Distinct();
        Assert.Equal(distinctWeeks.Count(), result.Count); // no duplicate weeks

        Assert.Equal(7, result.Count); // Check number of weeks

        // Check week 1 start and end
        var week1 = result[0];
        Assert.Equal(new DateOnly(2024, 01, 01), week1.WeekStartDate);
        Assert.Equal(week1.WeekStartDate.AddDays(6), week1.WeekEndDate);

        // Optional: Check a known mood for a week — e.g., week 1 (2024-01-01 to 2024-01-07)
        // Moods: 1x MoodId 1, 2x MoodId 2 (on Jan 1), 2x MoodId 1, 2x MoodId 4 (on Jan 2)
        // Tie between MoodId 3 and 4 — either valid
        Assert.Contains(week1.MoodId, new[] { 1, 4 });

        // Check week 3 (2024-01-15 to 2024-01-21) has no entries
        // Should not appear in result if no moods for that week
        //Assert.DoesNotContain(result, r => r.WeekStartDate == week3Start);
    }

    [Fact]
    public void CalculateMonthlyAverageMoodRating_ReturnsCorrectResults()
    {
        var data = TestDataSamples.GetSampleData();
        var result = MoodAnalyticsService.CalculateMonthlyAverageMoodRating(data).ToList();

        // Ensure one entry per (year, month)
        var distinctMonths = result.Select(r => (r.Year, r.Month)).Distinct();
        Assert.Equal(distinctMonths.Count(), result.Count); // no duplicates

        // Confirm expected months are present
        var expectedMonths = new[] { (2024, 1), (2024, 2), (2024, 3) };

        foreach (var (year, month) in expectedMonths)
        {
            Assert.Contains(result, r => r.Year == year && r.Month == month);
        }

        // Jan 2024: MoodId 2 appears 2 times (on Jan 1)
        var jan = result.FirstOrDefault(r => r.Year == 2024 && r.Month == 1);
        Assert.NotNull(jan);
        Assert.Equal(3, jan.MoodId);

        // Feb 2024: MoodId 2 appears 2 times (on Feb 1)
        var feb = result.FirstOrDefault(r => r.Year == 2024 && r.Month == 2);
        Assert.NotNull(feb);
        Assert.Equal(2, feb.MoodId);

        // Mar 2024: MoodId 3 appears 5 times (Mar 2: 2x, Mar 8: 1x, Mar 9: 2x)
        var mar = result.FirstOrDefault(r => r.Year == 2024 && r.Month == 3);
        Assert.NotNull(mar);
        Assert.Equal(3, mar.MoodId);
    }

    [Fact]
    public void CalculateRatings_WithEmptyInput_ReturnsEmpty()
    {
        var empty = new List<UserDailyMood>();

        Assert.Empty(MoodAnalyticsService.CalculateDailyAverageMoodRating(empty));
        Assert.Empty(MoodAnalyticsService.CalculateWeeklyAverageMoodRating(new DateOnly(2025, 01, 01), empty));
        Assert.Empty(MoodAnalyticsService.CalculateMonthlyAverageMoodRating(empty));
    }
}
