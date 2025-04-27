namespace MyMood.Api.Requests;

public class GetWeeklyAverageRatingsRequest
{
    public required DateTime FromDate { get; set; }

    public required DateTime ToDate { get; set; }
}
