using Core.Models;
using Infrastructure.Data;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class PollService(ApplicationDbContext applicationDbContext
                            , ILogger<PollService> logger
                            , IMapper mapper) : IPollService
    {
        private readonly ApplicationDbContext _Context = applicationDbContext;
        private readonly ILogger<PollService> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private DbSet<Poll> entity => _Context.Set<Poll>();

        public async Task<PollResponse?> GetPollAsync(string id , CancellationToken cancellationToken)
        {
            var poll = await entity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return null;
            }

            var response = _mapper.Map<PollResponse>(poll);
            return response;
        } 

        public async Task<IEnumerable<PollResponse>> ListPollsAsync(CancellationToken cancellationToken)
        {
            var polls = await entity.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PollResponse>>(polls);
        }

        public async Task<string> CreatePollAsync(PollRequest request, CancellationToken cancellationToken)
        {
            var poll = _mapper.Map<Poll>(request);

            poll.Id = Guid.NewGuid().ToString();
            await entity.AddAsync(poll, cancellationToken);
            await _Context.SaveChangesAsync();

            return poll.Id;
        }

        public async Task<PollResponse?> UpdatePollAsync(string id, PollRequest request, CancellationToken cancellationToken)
        {
            var poll = await entity.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return null;
            }
            _mapper.Map(request, poll);
            entity.Update(poll);
            await _Context.SaveChangesAsync();
            return _mapper.Map<PollResponse>(poll);
        }

        public async Task<bool> DeletePollAsync(string id, CancellationToken cancellationToken)
        {
            var poll = await entity.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return false;
            }

            entity.Remove(poll);
            await _Context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> TogglePublishStatusAsync(string id, CancellationToken cancellationToken) 
        {
            var poll =await entity.FirstOrDefaultAsync(x => x.Id == id, cancellationToken );

            if (poll is null)
            {
                _logger.LogWarning("Poll with id {Id} not found.", id);
                return  false;
            }

            poll.IsPublished = !poll.IsPublished;
            entity.Update(poll);
            await _Context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
