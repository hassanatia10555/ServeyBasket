using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;

namespace SurveyBasket.Infrastructure.Services.EmailService.Abstractions.Email
{
    public abstract class EmailMessageBodyBuilder<TtemplateModel>
        : IMessageBodyBuilder<TtemplateModel> where TtemplateModel : class
    {
        private string _templatePath = string.Empty;
        private string _body = string.Empty;
        protected abstract string TemplateName { get; }

        public string Build(TtemplateModel model)
        {

            ReadTemplate();
            ReadFileStream();
            AssignTemplateModel(model);

            return _body;
        }
        private EmailMessageBodyBuilder<TtemplateModel> ReadTemplate()
        {
            var path =
                $"D:\\Users\\hassan\\Desktop\\My\\Projects\\ServeyBasket\\SurveyBasket.Infrastructure\\Templates";

            _templatePath = Path.Combine(
                path,
                $"{TemplateName}.html");

            return this;
        }
        private EmailMessageBodyBuilder<TtemplateModel> ReadFileStream()
        {
            using var streamReader = new StreamReader(_templatePath);

            _body = streamReader.ReadToEnd();

            return this;
        }
        private void AssignTemplateModel(TtemplateModel templateModel)
        {
            foreach (var property in typeof(TtemplateModel).GetProperties())
            {
                var value = property.GetValue(templateModel);

                _body = _body.Replace(
                    $"{{{{{property.Name}}}}}",
                    value?.ToString() ?? string.Empty);
            }
        }
    }
}
