namespace SurveyBasket.Domain.Errors
{
    public static class VoteErrors
    {
        public static readonly Error VoteNotFound
            = new(StatusCodes.Status404NotFound,
                   "Vote With entered Id Not Found !..");
        public static readonly Error UserAlreadyVoted
            = new(StatusCodes.Status400BadRequest,
                   "User Has Already Voted In This Poll !..");
        public static readonly Error InvalidNumberOfQuestions
            = new(StatusCodes.Status400BadRequest,
                   "You have submitted an invalid number of questions !..");
        public static readonly Error InvalidQuestionId
            = new(StatusCodes.Status400BadRequest,
                   "There exist some invalid question ids !..");
        public static readonly Error InvalidAnswerId
            = new(StatusCodes.Status400BadRequest,
                   "There exist some invalid answer ids !..");
    }
}
