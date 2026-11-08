namespace SurveyBasket.Application.Contracts.Question;

public record QuestionResponse(
    Guid Id,
    string Content,
    ICollection<AnswerResponse> Answers);
