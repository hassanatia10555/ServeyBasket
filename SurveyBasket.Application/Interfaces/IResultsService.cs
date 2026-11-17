namespace SurveyBasket.Application.Interfaces
{
    public interface IResultsService
    {
        Task<Result<PollVotesResponse>> GetPollVotesAsync(string pollId, CancellationToken cancellationToken);
    }
}
