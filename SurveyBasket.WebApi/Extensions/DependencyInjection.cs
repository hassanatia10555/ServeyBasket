
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Security.Claims;

namespace SurveyBasket.WebApi.Extensions
{
    public static class DependencyInjection
    {
        public static void AddWebApiServices(this IServiceCollection services)
        {
            // Register Web API services here
        }   

        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null )
            {
                return string.Empty; // or throw an exception, depending on your requirements
            }
            return userIdClaim.Value;
        }
    }
}
