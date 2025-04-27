namespace MyMood.Api.Requests;

public class GetDailyAverageRatingsRequest
{
    public required DateTime FromDate { get; set; }

    public required DateTime ToDate { get; set; }
}
