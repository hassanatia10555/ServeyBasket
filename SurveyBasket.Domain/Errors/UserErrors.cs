namespace SurveyBasket.Domain.Errors
{
    public static class UserErrors
    {
        public static readonly Error IvalidCredentials
            = new(StatusCodes.Status401Unauthorized, "Entered Invalid Email/Password !..");
        public static readonly Error UserNotFound
            = new(StatusCodes.Status404NotFound, "User Not Found !..");
        public static readonly Error InvalidJwtToken
            = new(StatusCodes.Status400BadRequest, "Entered Invalid Jwt Token !..");
        public static readonly Error InvalidRefreshToken
            = new(StatusCodes.Status400BadRequest, "Entered Invalid Refresh Token !..");
        public static readonly Error RevokingToken
            = new(StatusCodes.Status400BadRequest, "Some Errors Hpeend Wihle Revoking Token !..");
        public static readonly Error UserAlreadyExists
            = new(StatusCodes.Status409Conflict, "User Already Exists !..");
        public static readonly Error EmailNotConfirmed
            = new(StatusCodes.Status401Unauthorized, "Email Is Not Confirmed !..");
        public static readonly Error DuplicatedEmail
            = new(StatusCodes.Status409Conflict, "Email Is Already Taken !..");
        public static readonly Error DuplicatedConfirmation
            = new(StatusCodes.Status409Conflict, "Email Confirmation Already Sent !..");
        public static readonly Error InvalidCode
            = new(StatusCodes.Status400BadRequest, "Invalid Confirmation Code !..");
    }
}
