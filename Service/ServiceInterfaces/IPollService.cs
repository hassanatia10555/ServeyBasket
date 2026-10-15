
namespace Service.ServiceInterfaces
{
    public interface IPollService 
    {
        Task<PollResponse?> GetPollAsync(string id);
        Task<IEnumerable<PollResponse>> ListPollsAsync();
        Task<string> CreatePollAsync(PollRequest request);
        Task UpdatePollAsync(string id, PollRequest request);
        Task DeletePollAsync(string id);
    }
}
