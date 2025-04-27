namespace MyMood.Api.Requests;

public class GetMonthlyAverageRatingsRequest
{
    public required DateTime FromDate { get; set; }

    public required DateTime ToDate { get; set; }
}
