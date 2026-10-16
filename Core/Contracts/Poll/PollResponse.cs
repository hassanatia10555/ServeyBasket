namespace Core.Contracts.Poll
{
    public record PollResponse
    (
        string Id,
        string Title,
        string Summary,
        DateTime? StartsAt,
        DateTime? EndsAt,
        bool IsPublished
    );
}
