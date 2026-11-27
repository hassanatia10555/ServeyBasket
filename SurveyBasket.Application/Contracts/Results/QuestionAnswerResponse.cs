namespace SurveyBasket.Application.Contracts.Results
{
    public record QuestionAnswerResponse(
        string QuestionTitle,
        string Answer
    );
}
