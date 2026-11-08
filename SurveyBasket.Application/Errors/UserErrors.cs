namespace SurveyBasket.Application.Errors
{
    public static class UserErrors
    {
        public static readonly Error IvalidCredentials
            = new ("Invalid Credentials", "Entered Invalid Email/Password !..");

        public static readonly Error UserNotFound
            = new ("User Not Found", "User Not Found !..");

        public static readonly Error InvalidJwtToken
            = new ("Invalid Jwt Token", "Entered Invalid Jwt Token !..");
        public static readonly Error InvalidRefreshToken
            = new ("Invalid Refresh Token", "Entered Invalid Refresh Token !..");   
        public static readonly Error RevokingToken
            = new ("Token Not Revoked", "Some Errors Hpeend Wihle Revoking Token !..");   
    }
}
