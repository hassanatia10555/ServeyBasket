using Core.Models;
using Infrastructure.Data;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service
{
    public class PollService(ApplicationDbContext applicationDbContext
                            , ILogger<PollService> logger
                            , IMapper mapper) : IPollService
    {
        private readonly ApplicationDbContext _Context = applicationDbContext;
        private readonly ILogger<PollService> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private DbSet<Poll> entity => _Context.Set<Poll>();
        public Task<PollResponse?> GetPollAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<PollResponse>> ListPollsAsync()
        {
            throw new NotImplementedException();
        }
        public Task<string> CreatePollAsync(PollRequest request)
        {
            throw new NotImplementedException();
        }
        public Task UpdatePollAsync(string id, PollRequest request)
        {
            throw new NotImplementedException();
        }
        public Task DeletePollAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
