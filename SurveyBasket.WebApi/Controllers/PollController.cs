using SurveyBasket.Application.Abstractions;
using SurveyBasket.Application.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController(IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _pollService.GetPollAsync(id, cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value)
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var polls = await _pollService.ListPollsAsync(cancellationToken);
            return Ok(polls);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollService.CreatePollAsync(request, cancellationToken);
            return result.IsSuccess ?
                CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
               : result.Error == PollErrors.PollNotFound ?
                     result.ToProblem(StatusCodes.Status404NotFound) :
                     result.ToProblem(StatusCodes.Status409Conflict);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdatePollAsync(id, request, cancellationToken);
            
            return result.IsSuccess ?
                NoContent()
               : result.Error == PollErrors.PollNotFound ?
                     result.ToProblem(StatusCodes.Status404NotFound) :
                     result.ToProblem(StatusCodes.Status409Conflict);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _pollService.DeletePollAsync(id, cancellationToken);
            
            return result.IsSuccess ?
                NoContent()
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }

        [HttpPut("{id}/TogglePublish")]
        public async Task<IActionResult> TogglePublishStatus([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
            
            return result.IsSuccess ?
                NoContent()
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }
    }

}