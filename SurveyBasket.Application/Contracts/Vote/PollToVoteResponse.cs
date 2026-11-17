using SurveyBasket.Application.Contracts.Question;

namespace SurveyBasket.Application.Contracts.Vote
{
    public record PollToVoteResponse(
        string PollId,
        ICollection<QuestionResponse> Questions
    );
}
