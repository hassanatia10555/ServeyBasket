using Microsoft.AspNetCore.Http;

namespace SurveyBasket.Application.Abstractions
{
    public record Error( int Code , string Description)
    {
        public readonly static Error None
            = new(0, string.Empty);
    }
}
