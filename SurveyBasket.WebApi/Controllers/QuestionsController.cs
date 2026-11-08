using SurveyBasket.Application.Abstractions;

namespace WebApi.Controllers;

[Route("api/polls/{pollId:guid}/questions")]
[ApiController]
[Authorize]
public class QuestionsController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    [HttpGet("{questionId:guid}")]
    public async Task<IActionResult> Get(Guid pollId, Guid questionId, CancellationToken cancellationToken)
    {
        var result = await _questionService.GetQuestionAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : ToProblem(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid pollId, CancellationToken cancellationToken)
    {
        var result = await _questionService.ListQuestionsAsync(pollId, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : ToProblem(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid pollId, QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionService.CreateQuestionAsync(pollId, request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { pollId, questionId = result.Value.Id }, result.Value)
            : ToProblem(result);
    }

    [HttpPut("{questionId:guid}")]
    public async Task<IActionResult> Update(Guid pollId, Guid questionId, QuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _questionService.UpdateQuestionAsync(pollId, questionId, request, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : ToProblem(result);
    }

    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> Delete(Guid pollId, Guid questionId, CancellationToken cancellationToken)
    {
        var result = await _questionService.DeleteQuestionAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : ToProblem(result);
    }

    [HttpPut("{questionId:guid}/ToggleActive")]
    public async Task<IActionResult> ToggleActiveStatus(Guid pollId, Guid questionId, CancellationToken cancellationToken)
    {
        var result = await _questionService.ToggleActiveStatusAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : ToProblem(result);
    }

    private static ObjectResult ToProblem(Result result)
    {
        var statusCode = result.Error.Code switch
        {
            "Invalid Question" => StatusCodes.Status400BadRequest,
            "Answer In Use" => StatusCodes.Status409Conflict,
            "Answer Not Found" => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status404NotFound
        };

        return result.ToProblem(statusCode);
    }
}
