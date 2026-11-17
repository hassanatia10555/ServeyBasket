namespace SurveyBasket.Domain.Interfaces;

public interface IEmailBodyBuilder
{
     string Build(object model);
}
