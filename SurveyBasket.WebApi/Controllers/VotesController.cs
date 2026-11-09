using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Application.Abstractions;
using SurveyBasket.Application.Contracts.Vote;
using SurveyBasket.WebApi.Extensions;

namespace SurveyBasket.WebApi.Controllers
{
    [Route("api/Polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class VotesController(IVoteService voteService) : ControllerBase
    {
        private readonly IVoteService _voteService = voteService;

        [HttpGet("start")]
        public async Task<IActionResult> StartVoteAsync(string pollId, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _voteService.ListPollToVoteAsync(userId, pollId, cancellationToken);

            return result.IsSuccess
              ? Ok(result.Value)
              : result.ToProblem();
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateVoteAsync(string pollId, VoteRequest request, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _voteService.CreateVoteAsync(userId, pollId, request, cancellationToken);

            return result.IsSuccess
              ? Created()
              : result.ToProblem();
        }
    }
}
