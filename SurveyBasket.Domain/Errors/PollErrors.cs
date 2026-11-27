namespace SurveyBasket.Domain.Errors
{
    public static class PollErrors
    {
        public static readonly Error PollNotFound
            = new(StatusCodes.Status404NotFound,
                   "Poll With entered Id Not Found !..");

        public static readonly Error PollAlreadyExists
            = new(StatusCodes.Status409Conflict,
                   "Poll With entered Title Already Exists !..");
        public static readonly Error PollIsNotActive
            = new(StatusCodes.Status400BadRequest,
                   "Poll Is Not Active !..");
    }
}
