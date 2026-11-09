namespace SurveyBasket.Application.Interfaces;

public interface IQuestionService
{
    Task<Result<QuestionResponse>> GetQuestionAsync(string pollId, string questionId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<QuestionResponse>>> ListQuestionsAsync(string pollId, CancellationToken cancellationToken);
    Task<Result<QuestionResponse>> CreateQuestionAsync(string pollId, QuestionRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateQuestionAsync(string pollId, string questionId, QuestionRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteQuestionAsync(string pollId, string questionId, CancellationToken cancellationToken);
    Task<Result> ToggleActiveStatusAsync(string pollId, string questionId, CancellationToken cancellationToken);
}
