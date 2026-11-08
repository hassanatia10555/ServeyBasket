namespace SurveyBasket.Application.Contracts.Question;

public record QuestionRequest(
    string Content,
    ICollection<AnswerRequest> Answers);
