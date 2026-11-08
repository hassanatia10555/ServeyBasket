namespace SurveyBasket.Application.Interfaces;

public interface IQuestionService
{
    Task<Result<QuestionResponse>> GetQuestionAsync(Guid pollId, Guid questionId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<QuestionResponse>>> ListQuestionsAsync(Guid pollId, CancellationToken cancellationToken);
    Task<Result<QuestionResponse>> CreateQuestionAsync(Guid pollId, QuestionRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateQuestionAsync(Guid pollId, Guid questionId, QuestionRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteQuestionAsync(Guid pollId, Guid questionId, CancellationToken cancellationToken);
    Task<Result> ToggleActiveStatusAsync(Guid pollId, Guid questionId, CancellationToken cancellationToken);
}
