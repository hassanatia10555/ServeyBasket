using Core.Contracts.Poll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController(IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var poll = await _pollService.GetPollAsync(id);
            if (poll is null)
            {
                return NotFound();
            }
            return Ok(poll);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var polls = await _pollService.ListPollsAsync();
            return Ok(polls);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PollRequest request)
        {
            var id = await _pollService.CreatePollAsync(request);            
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] PollRequest request)
        {
            var poll = await _pollService.UpdatePollAsync(id, request);
            if (poll is null)
            {
                return BadRequest($"Poll With Given ID {id} Not Found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _pollService.DeletePollAsync(id);
            if (!result)
            {
                return BadRequest($"Poll With Given ID {id} Not Found");
            }
            return NoContent();
        }
    }

}