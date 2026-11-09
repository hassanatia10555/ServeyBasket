using SurveyBasket.Application.Abstractions;

namespace WebApi.Controllers;

[Route("api/polls/{pollId}/questions")]
[ApiController]
[Authorize]
public class QuestionsController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    [HttpGet("{questionId:guid}")]
    public async Task<IActionResult> Get(string pollId, string questionId, CancellationToken cancellationToken)
    {
        var result = await _questionService.GetQuestionAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string pollId, CancellationToken cancellationToken)
    {
        var result = await _questionService.ListQuestionsAsync(pollId, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string pollId, QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionService.CreateQuestionAsync(pollId, request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new {  pollId, questionId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{questionId:guid}")]
    public async Task<IActionResult> Update(string pollId, string questionId, QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionService.UpdateQuestionAsync(pollId, questionId, request, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> Delete(string pollId, string questionId, CancellationToken cancellationToken)
    {
        var result = await _questionService.DeleteQuestionAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpPut("{questionId:guid}/ToggleActive")]
    public async Task<IActionResult> ToggleActiveStatus(string pollId, string questionId, CancellationToken cancellationToken)
    {
        var result = await _questionService.ToggleActiveStatusAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }
}
