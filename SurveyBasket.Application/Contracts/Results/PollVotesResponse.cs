namespace SurveyBasket.Application.Contracts.Results
{
    public record PollVotesResponse(
        string PollTitle,
        IEnumerable<VoteResponse> Votes
    );
}
