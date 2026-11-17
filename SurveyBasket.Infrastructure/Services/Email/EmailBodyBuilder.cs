namespace SurveyBasket.Infrastructure.Services.Email;

public abstract class EmailBodyBuilder<TModel>: IEmailBodyBuilder 
{
    private string _templatePath = string.Empty;
    private string _body = string.Empty;
    protected abstract string TemplateName { get; }
    public string Build(TModel model)
    {
        ReadTemplate();
        ReadFileStream();
        AssignTemplateModel(model);

        return _body;
    }

    string IEmailBodyBuilder.Build(object model)
    {
        if (model is not TModel typedModel)
            throw new ArgumentException(
                $"Invalid model type. Expected {typeof(TModel).Name}");

        return Build(typedModel);
    }

    private EmailBodyBuilder<TModel> ReadTemplate()
    {
        var path =
            $"D:\\Users\\hassan\\Desktop\\My\\Projects\\ServeyBasket\\SurveyBasket.Infrastructure\\Templates";

        _templatePath = Path.Combine(
            path,
            $"{TemplateName}.html");

        return this;
    }

    private EmailBodyBuilder<TModel> ReadFileStream()
    {
        using var streamReader = new StreamReader(_templatePath);

        _body = streamReader.ReadToEnd();

        return this;
    }

    private void AssignTemplateModel(TModel templateModel)
    {
        foreach (var property in typeof(TModel).GetProperties())
        {
            var value = property.GetValue(templateModel);

            _body = _body.Replace(
                $"{{{{{property.Name}}}}}",
                value?.ToString() ?? string.Empty);
        }
    }
}