namespace SurveyBasket.Application.Contracts.Poll
{
    public record PollRequest(
        string Title,
        string Summary,
        DateTime? StartsAt,
        DateTime? EndsAt,
        bool IsPublished
    );
}
