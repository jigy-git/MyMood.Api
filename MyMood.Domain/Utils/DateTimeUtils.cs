namespace MyMood.Domain.Utils;

public static class DateTimeUtils
{
    public static DateOnly ConvertToUtcDateOnly(this DateTime input)
    {
        // Convert to UTC if not already
        var utcDateTime = input.Kind == DateTimeKind.Utc ? input : input.ToUniversalTime();

        // Extract the DateOnly part from the UTC DateTime
        return DateOnly.FromDateTime(utcDateTime);
    }

    public static DateOnly ConvertUtcToLocalDateOnly(this DateTime utcDateTime)
    {
        // Convert UTC to local time
        DateTime localDateTime = utcDateTime.ToLocalTime();

        // Extract DateOnly
        return DateOnly.FromDateTime(localDateTime);
    }
}
