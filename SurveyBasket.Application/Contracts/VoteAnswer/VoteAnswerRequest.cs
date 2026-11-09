namespace SurveyBasket.Application.Contracts.VoteAnswer
{
    public record VoteAnswerRequest(
       string QuestionId,
       string AnswerId
    );
}
