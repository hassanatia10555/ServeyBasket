namespace SurveyBasket.Application.Contracts.Poll
{
    public record PollResponse
    (
        Guid Id,
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
