using SurveyBasket.Application.Contracts.Answer;

namespace SurveyBasket.Application.Contracts.Question;

public record QuestionResponse(
    string Id,
    string Content,
    ICollection<AnswerResponse> Answers);
