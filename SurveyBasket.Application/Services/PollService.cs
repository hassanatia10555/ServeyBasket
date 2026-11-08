using SurveyBasket.Application.Errors;

namespace SurveyBasket.Application.Services
{
    public class PollService(IApplicationDbContext applicationDbContext
                            , ILogger<PollService> logger
                            , IMapper mapper) : IPollService
    {
        private readonly IApplicationDbContext _Context = applicationDbContext;
        private readonly ILogger<PollService> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private DbSet<Poll> entity => _Context.Set<Poll>();

        public async Task<Result<PollResponse>> GetPollAsync(Guid id, CancellationToken cancellationToken)
        {
            var poll = await entity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return Result.Failure<PollResponse>(PollErrors.PollNotFound);
            }

            var response = _mapper.Map<PollResponse>(poll);

            return Result.Success<PollResponse>(response);
        }

        public async Task<IEnumerable<PollResponse>> ListPollsAsync(CancellationToken cancellationToken)
        {
            var polls = await entity.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PollResponse>>(polls);
        }

        public async Task<Result<PollResponse>> CreatePollAsync(PollRequest request, CancellationToken cancellationToken)
        {
            var existingPoll = await entity.AsNoTracking().FirstOrDefaultAsync(x => x.Title == request.Title, cancellationToken);
            if (existingPoll is not null)
            {
                _logger.LogWarning("Poll with title {Title} already exists.", request.Title);
                return Result.Failure<PollResponse>(PollErrors.PollAlreadyExists);
            }

            var poll = _mapper.Map<Poll>(request);

            await entity.AddAsync(poll, cancellationToken);
            await _Context.SaveChangesAsync(cancellationToken);

            return Result.Success<PollResponse>(_mapper.Map<PollResponse>(poll));
        }

        public async Task<Result> UpdatePollAsync(Guid id, PollRequest request, CancellationToken cancellationToken)
        {
            var poll = await entity.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            
            if (poll is null)
            {
                _logger.LogError("Poll with id {Id} not found.", id);
                return Result.Failure(error:PollErrors.PollNotFound);
            }
           
            if (poll.Title != request.Title)
            {
                var existingPoll = await entity.AsNoTracking().FirstOrDefaultAsync(x => x.Title == request.Title, cancellationToken);
                if (existingPoll is not null)
                {
                    _logger.LogWarning("Poll with title {Title} already exists.", request.Title);
                    return Result.Failure(error: PollErrors.PollAlreadyExists);
                }
            }
            _mapper.Map(request, poll);
            entity.Update(poll);
            await _Context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeletePollAsync(Guid id, CancellationToken cancellationToken)
        {
            var poll = await entity.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return Result.Failure(PollErrors.PollNotFound);

            }

            entity.Remove(poll);
            await _Context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> TogglePublishStatusAsync(Guid id, CancellationToken cancellationToken)
        {
            var poll = await entity.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return Result.Failure(error: PollErrors.PollNotFound);
            }

            poll.IsPublished = !poll.IsPublished;
            entity.Update(poll);
            await _Context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }        
    }
}
