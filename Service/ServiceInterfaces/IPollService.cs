
namespace Service.ServiceInterfaces
{
    public interface IPollService 
    {
        Task<PollResponse?> GetPollAsync(string id , CancellationToken cancellationToken);
        Task<IEnumerable<PollResponse>> ListPollsAsync(CancellationToken cancellationToken);
        Task<string> CreatePollAsync(PollRequest request , CancellationToken cancellationToken);
        Task<PollResponse?> UpdatePollAsync(string id, PollRequest request , CancellationToken cancellationToken);
        Task<bool> DeletePollAsync(string id , CancellationToken cancellationToken);
        Task<bool> TogglePublishStatusAsync(string id, CancellationToken cancellationToken);
    }
}
