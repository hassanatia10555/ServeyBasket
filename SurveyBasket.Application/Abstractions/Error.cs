namespace SurveyBasket.Application.Abstractions
{
    public record Error(string Code , string Description)
    {
        public readonly static Error None
            = new(string.Empty, string.Empty);
    }
}
