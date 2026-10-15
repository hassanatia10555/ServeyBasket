
namespace Service.ServiceInterfaces
{
    public interface IPollService 
    {
        Task<PollResponse?> GetPollAsync(string id);
        Task<IEnumerable<PollResponse>> ListPollsAsync();
        Task<string> CreatePollAsync(PollRequest request);
        Task<PollResponse?> UpdatePollAsync(string id, PollRequest request);
        Task<bool> DeletePollAsync(string id);
    }
}
