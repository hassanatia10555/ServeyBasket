using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Application.Contracts.Vote
{
    public record PollToVoteResponse(
        string PollId,
        ICollection<QuestionResponse> Questions
    );
}
