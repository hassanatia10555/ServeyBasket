namespace SurveyBasket.Application.Abstractions
{
    public static class UserErrors
    {
        public static readonly Error IvalidCredentials
            = new (StatusCodes.Status401Unauthorized, "Entered Invalid Email/Password !..");
        public static readonly Error UserNotFound
            = new (StatusCodes.Status404NotFound, "User Not Found !..");
        public static readonly Error InvalidJwtToken
            = new (StatusCodes.Status400BadRequest, "Entered Invalid Jwt Token !..");
        public static readonly Error InvalidRefreshToken
            = new (StatusCodes.Status400BadRequest, "Entered Invalid Refresh Token !..");   
        public static readonly Error RevokingToken
            = new (StatusCodes.Status400BadRequest, "Some Errors Hpeend Wihle Revoking Token !..");   
    }
}
