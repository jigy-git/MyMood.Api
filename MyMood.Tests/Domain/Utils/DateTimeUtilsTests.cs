namespace MyMood.Tests.Domain.Utils;

using MyMood.Domain.Utils;
using System;
using Xunit;

public class DateTimeUtilsTests
{
    [Fact]
    public void ConvertToUtcDateOnly_WithUtcInput_ReturnsSameDate()
    {
        // Arrange
        var date = new DateTime(2024, 12, 25, 10, 0, 0, DateTimeKind.Utc);

        // Act
        var result = date.ConvertToUtcDateOnly();

        // Assert
        Assert.Equal(new DateOnly(2024, 12, 25), result);
    }

    [Fact]
    public void ConvertToUtcDateOnly_WithLocalInput_AdjustsToUtc()
    {
        // Arrange
        var localTime = new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local);
        var expectedUtcDate = DateOnly.FromDateTime(localTime.ToUniversalTime());

        // Act
        var result = localTime.ConvertToUtcDateOnly();

        // Assert
        Assert.Equal(expectedUtcDate, result);
    }

    [Fact]
    public void ConvertToUtcDateOnly_WithUnspecifiedInput_TreatsAsLocal()
    {
        // Arrange
        var unspecified = new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Unspecified);
        var expectedUtcDate = DateOnly.FromDateTime(DateTime.SpecifyKind(unspecified, DateTimeKind.Local).ToUniversalTime());

        // Act
        var result = unspecified.ConvertToUtcDateOnly();

        // Assert
        Assert.Equal(expectedUtcDate, result);
    }

    [Fact]
    public void ConvertUtcToLocalDateOnly_WithUtcInput_ReturnsLocalDate()
    {
        // Arrange
        var utc = new DateTime(2024, 12, 25, 23, 0, 0, DateTimeKind.Utc); // Depending on TZ, this could roll over

        // Act
        var result = utc.ConvertUtcToLocalDateOnly();

        // Assert
        var expected = DateOnly.FromDateTime(utc.ToLocalTime());
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertUtcToLocalDateOnly_MidnightUtc_ReturnsCorrectLocalDate()
    {
        // Arrange
        var utc = new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = utc.ConvertUtcToLocalDateOnly();

        // Assert
        var expected = DateOnly.FromDateTime(utc.ToLocalTime());
        Assert.Equal(expected, result);
    }
}
