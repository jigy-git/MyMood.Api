﻿namespace MyMood.Domain;

using MyMood.Domain.Models;
using MyMood.Domain.Utils;

/// <summary>
/// This service provides methods to calculate average mood ratings based on user submissions.
/// To be done: What happens if there are equal mood counts? across days or weeks, or years
/// </summary>
public static class MoodAnalyticsService
{
    /// <summary>
    /// Gets the most frequent mood submitted by users (e.g. “Pretty good”) for a day,
    /// Average mood today: Pretty good (MoodId = 3, selected by 12 users)
    /// </summary>
    /// <param name="userMoods"></param>
    /// <returns></returns>
    public static IEnumerable<AverageMoodForDay> CalculateDailyAverageMoodRating(
        IEnumerable<UserDailyMood> userMoods
    )
    {
        if (userMoods == null || !userMoods.Any())
            return Enumerable.Empty<AverageMoodForDay>();

        return userMoods
            .GroupBy(m => m.Date) // Group by date
            .Select(g =>
            {
                var mostFrequentMood = g.GroupBy(m => m.MoodId) // Group again by MoodId for each day
                    .Select(mg => new { MoodId = mg.Key, Count = mg.Count() })
                    .OrderByDescending(mg => mg.Count)
                    .First(); // Get the mood with the highest frequency

                return new AverageMoodForDay { Date = g.Key, MoodId = mostFrequentMood.MoodId };
            });
    }

    /// <summary>
    /// Gets most common mood for the week
    /// Week 1 starts at the earliest date in list, each week is a fixed 7 - day block after that.
    /// </summary>
    /// <param name="userMoods"></param>
    /// <returns></returns>
    public static IEnumerable<AverageMoodForWeek> CalculateWeeklyAverageMoodRating(
      DateOnly startDate,
      IEnumerable<UserDailyMood> userMoods
  )
    {
        if (userMoods == null || !userMoods.Any())
            return Enumerable.Empty<AverageMoodForWeek>();

        // Group moods into weeks starting from startDate
        var moodsGroupedByWeek = userMoods
             .GroupBy(m =>
             {
                 var daysSinceStart = (m.Date.DayNumber - startDate.DayNumber);
                 return daysSinceStart / 7; 
             });

        return moodsGroupedByWeek.Select(g =>
        {
            // Find the most frequent MoodId in this group
            var mostFrequentMood = g
                .GroupBy(m => m.MoodId)
                .OrderByDescending(mg => mg.Count())
                .First()
                .Key;

            // Calculate start and end of the week
            var weekIndex = g.Key;
            var weekStart = startDate.AddDays(weekIndex * 7) ;
            var weekEnd = weekStart.AddDays(6);

            return new AverageMoodForWeek
            {
                WeekStartDate = weekStart,
                WeekEndDate = weekEnd,
                MoodId = mostFrequentMood
            };
        });
    }

    /// <summary>
    /// Gets Average Mood Rating for each calendar month between start & end of the dates provided
    /// </summary>
    /// <param name="userMoods"></param>
    /// <returns></returns>
    public static IEnumerable<AverageMoodForMonth> CalculateMonthlyAverageMoodRating(
        IEnumerable<UserDailyMood> userMoods
    )
    {
        if (userMoods == null || !userMoods.Any())
            return Enumerable.Empty<AverageMoodForMonth>();

        return userMoods
            .GroupBy(m => new { m.Date.Year, m.Date.Month }) // Group by calendar month
            .Select(g =>
            {
                var mostFrequentMood = g.GroupBy(m => m.MoodId)
                    .Select(mg => new { MoodId = mg.Key, Count = mg.Count() })
                    .OrderByDescending(mg => mg.Count)
                    .First();

                return new AverageMoodForMonth
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MoodId = mostFrequentMood.MoodId,
                };
            })
            .OrderBy(r => r.Year)
            .ThenBy(r => r.Month);
    }
}
