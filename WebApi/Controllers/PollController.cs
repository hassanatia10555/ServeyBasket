using Core.Contracts.Poll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;

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
            var poll = await _pollService.GetPollAsync(id, cancellationToken);
            if (poll is null)
            {
                return NotFound();
            }
            return Ok(poll);
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
            var id = await _pollService.CreatePollAsync(request, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            PollResponse? poll = await _pollService.UpdatePollAsync(id, request, cancellationToken);
            if (poll is null)
            {
                return NotFound($"Poll With Given ID {id} Not Found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _pollService.DeletePollAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound($"Poll With Given ID {id} Not Found");
            }
            return NoContent();
        }
        [HttpPut("{id}/TogglePublish")]
        public async Task<IActionResult> TogglePublishStatus([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound($"Poll With Given ID {id} Not Found");
            }
            return NoContent();
        }
    }

}