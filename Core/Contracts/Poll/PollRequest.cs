namespace Core.Contracts.Poll
{
    public record PollRequest(
        string Title,
        string Summary,
        DateTime? StartsAt,
        DateTime? EndsAt
    );
}
