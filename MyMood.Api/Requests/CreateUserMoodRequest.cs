namespace MyMood.Api.Requests;

public class CreateUserMoodRequest
{
    public required int UserId { get; set; }
    public required int MoodId { get; set; }
    public string? Comment { get; set; }
}
