
namespace SurveyBasket.Application.Interfaces
{
    public interface IVoteService
    {
        Task<Result> CreateVoteAsync( string userId , string pollId ,  VoteRequest request, CancellationToken cancellationToken);
        //Task<VoteResponse> GetVoteAsync(Guid pollId , Guid voteId,  CancellationToken cancellationToken);
        Task<Result<PollToVoteResponse>> ListPollToVoteAsync( string userId , string pollId ,  CancellationToken cancellationToken);
    }
}
