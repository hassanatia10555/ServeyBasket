namespace SurveyBasket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultsController(IResultsService resultsService) : ControllerBase
    {
        private readonly IResultsService _resultsService = resultsService;

        [HttpGet]
        [Route("{pollId}/votes")]
        public async Task<IActionResult> GetPollVotes([FromRoute]string pollId, CancellationToken cancellationToken)
        {
            var result = await _resultsService.GetPollVotesAsync(pollId, cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value)
               : result.ToProblem();
        }
    }
}
