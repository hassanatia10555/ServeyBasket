using SurveyBasket.Application.Contracts.Answer;
using SurveyBasket.Application.Abstractions;

namespace SurveyBasket.Application.Services
{
    public class VoteService(IApplicationDbContext context) : IVoteService
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<Result> CreateVoteAsync(string userId, string pollId, VoteRequest request, CancellationToken cancellationToken)
        {
            var isExistingPoll = await _context.Set<Poll>()
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == pollId, cancellationToken);

            if (isExistingPoll is null)
            {
                return Result.Failure<PollToVoteResponse>(PollErrors.PollNotFound);
            }
            if (isExistingPoll?.Votes.Any(x => x.UserId == userId) ?? false)
            {
                return Result.Failure<PollToVoteResponse>(VoteErrors.UserAlreadyVoted);
            }
            if (!(isExistingPoll!.IsPublished &&
                (isExistingPoll.EndsAt >= DateTime.UtcNow &&
                isExistingPoll.StartsAt <= DateTime.UtcNow)))
            {
                return Result.Failure<PollToVoteResponse>(PollErrors.PollIsNotActive);
            }

            var existingQuestionIds = isExistingPoll.Questions
                .Where(x => x.IsActive)
                .Select(x => x.Id)
                .ToList();

            if(!request.VoteAnswerRequests.All(x => existingQuestionIds.Any(q => q == x.QuestionId)))
            {
                return Result.Failure<PollToVoteResponse>(VoteErrors.InvalidQuestionId);
            }
            if(existingQuestionIds.Count != request.VoteAnswerRequests.Count)
            {
                return Result.Failure<PollToVoteResponse>(VoteErrors.InvalidNumberOfQuestions);
            }

            var existingAnswerIds = isExistingPoll.Questions
                .Where(x => x.IsActive)
                .SelectMany(x => x.Answers.Where(a => a.IsActive).Select(a => a.Id))
                .ToList();

            if(!request.VoteAnswerRequests.All(x => existingAnswerIds.Contains(x.AnswerId)))
            {
                return Result.Failure<PollToVoteResponse>(VoteErrors.InvalidAnswerId);
            }

            var vote = new Vote
            {
                UserId = userId,
                PollId = pollId,
                SubmittedOn = DateTime.UtcNow,
                VoteAnswers = request.VoteAnswerRequests.Select(x => new VoteAnswer
                {
                    QuestionId = x.QuestionId,
                    AnswerId = x.AnswerId
                }).ToList()
            };

            await _context.Set<Vote>().AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<PollToVoteResponse>> ListPollToVoteAsync(string userId, string pollId, CancellationToken cancellationToken)
        {

            var isExistingPoll =await _context.Set<Poll>()
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == pollId , cancellationToken);

            if (isExistingPoll is null)
            {
                return Result.Failure<PollToVoteResponse>(PollErrors.PollNotFound);
            }
            // check if the user already voted in this poll
            if (isExistingPoll?.Votes.Any(x => x.UserId == userId) ?? false)
            {
                return Result.Failure<PollToVoteResponse>(VoteErrors.UserAlreadyVoted);
            }
            if(!(isExistingPoll!.IsPublished &&
                (isExistingPoll.EndsAt >= DateTime.UtcNow &&
                isExistingPoll.StartsAt <= DateTime.UtcNow))) 
            {
                return Result.Failure<PollToVoteResponse>(PollErrors.PollIsNotActive);
            }        

            return Result.Success(new PollToVoteResponse(
                pollId,
                isExistingPoll.Questions.Select(x => new QuestionResponse(
                    x.Id.ToString(),
                    x.Content,
                    x.Answers.Select(a => new AnswerResponse(
                        a.Id.ToString(),
                        a.Content)).ToList())).ToList()));
        }
    }
}
