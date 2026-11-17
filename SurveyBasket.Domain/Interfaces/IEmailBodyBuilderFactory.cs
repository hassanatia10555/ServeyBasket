namespace SurveyBasket.Domain.Interfaces;

public interface IEmailBodyBuilderFactory
{
    IEmailBodyBuilder Create(string templateName);
}
