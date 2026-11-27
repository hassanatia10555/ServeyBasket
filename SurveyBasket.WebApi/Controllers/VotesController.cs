namespace SurveyBasket.WebApi.Controllers
{
    [Route("api/Polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class VotesController(IVoteService voteService) : ControllerBase
    {
        private readonly IVoteService _voteService = voteService;

        [HttpGet("start")]
        public async Task<IActionResult> StartVoteAsync([FromRoute]string pollId, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _voteService.ListPollToVoteAsync(userId, pollId, cancellationToken);

            return result.IsSuccess
              ? Ok(result.Value)
              : result.ToProblem();
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateVoteAsync([FromRoute]string pollId, VoteRequest request, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _voteService.CreateVoteAsync(userId, pollId, request, cancellationToken);

            return result.IsSuccess
              ? Created()
              : result.ToProblem();
        }
    }
}
