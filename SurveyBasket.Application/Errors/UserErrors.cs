namespace SurveyBasket.Application.Errors
{
    public static class UserErrors
    {
        public static readonly Error IvalidCredentials
            = new Error("Invalid Credentials", "Entered Invalid Email/Password !..");

        public static readonly Error UserNotFound
            = new Error("User Not Found", "User Not Found !..");

        public static readonly Error InvalidJwtToken
            = new Error("Invalid Jwt Token", "Entered Invalid Jwt Token !..");
        public static readonly Error InvalidRefreshToken
            = new Error("Invalid Refresh Token", "Entered Invalid Refresh Token !..");   
        public static readonly Error RevokingToken
            = new Error("Token Not Revoked", "Some Errors Hpeend Wihle Revoking Token !..");   
    }
}
