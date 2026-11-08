namespace SurveyBasket.Application.Interfaces
{
    public interface IPollService 
    {
        Task<Result<PollResponse>> GetPollAsync(Guid id , CancellationToken cancellationToken);
        Task<IEnumerable<PollResponse>> ListPollsAsync(CancellationToken cancellationToken);
        Task<Result<PollResponse>> CreatePollAsync(PollRequest request , CancellationToken cancellationToken);
        Task<Result> UpdatePollAsync(Guid id, PollRequest request , CancellationToken cancellationToken);
        Task<Result> DeletePollAsync(Guid id , CancellationToken cancellationToken);
        Task<Result> TogglePublishStatusAsync(Guid id, CancellationToken cancellationToken);
    }
}
