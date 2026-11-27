namespace SurveyBasket.Application.Contracts.Results
{
    public record VoteResponse(
        string UserName,
        DateTime SubmissionOn,
        IEnumerable<QuestionAnswerResponse> QuestionAnswers
    );

}