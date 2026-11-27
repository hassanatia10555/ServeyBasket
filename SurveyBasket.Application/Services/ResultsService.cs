using SurveyBasket.Application.Contracts.Results;
using SurveyBasket.Domain.Interfaces;

namespace SurveyBasket.Application.Services
{
    public class ResultsService(IApplicationDbContext applicationDbContext) : IResultsService
    {
        public IApplicationDbContext _context = applicationDbContext;

        public async Task<Result<PollVotesResponse>> GetPollVotesAsync(string pollId , CancellationToken cancellationToken)
        {
            var poll = await _context.Set<Poll>()
                .Where(p => p.Id == pollId)
                     .Select(p => new PollVotesResponse(
                         p.Title,
                         p.Votes.Select(v => new VoteResponse(
                             UserName: $"{v.User.FirstName} {v.User.LastName}",
                             v.SubmittedOn,
                             v.VoteAnswers.Select(qv => new QuestionAnswerResponse(
                                 qv.Question.Content,
                                 qv.Answer.Content
                             ))
                         ))
                     ))
              .SingleOrDefaultAsync(cancellationToken);

            return poll is null
               ? Result.Failure<PollVotesResponse>(PollErrors.PollNotFound)
               : Result.Success(poll);
        }
    }
}
