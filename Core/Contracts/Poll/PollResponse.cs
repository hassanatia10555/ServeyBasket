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
        //string CreatedBy,
        //string CreatedOn,
        //string UpdatedBy,
        //string UpdatedOn
    );
}
