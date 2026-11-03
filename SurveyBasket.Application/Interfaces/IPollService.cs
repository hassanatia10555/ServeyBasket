namespace SurveyBasket.Application.Interfaces
{
    public interface IPollService 
    {
        Task<Result<PollResponse>> GetPollAsync(string id , CancellationToken cancellationToken);
        Task<IEnumerable<PollResponse>> ListPollsAsync(CancellationToken cancellationToken);
        Task<Result<PollResponse>> CreatePollAsync(PollRequest request , CancellationToken cancellationToken);
        Task<Result> UpdatePollAsync(string id, PollRequest request , CancellationToken cancellationToken);
        Task<Result> DeletePollAsync(string id , CancellationToken cancellationToken);
        Task<Result> TogglePublishStatusAsync(string id, CancellationToken cancellationToken);
    }
}
