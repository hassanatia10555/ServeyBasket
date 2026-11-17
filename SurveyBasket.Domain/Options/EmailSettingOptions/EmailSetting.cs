namespace SurveyBasket.Domain.Options.EmailSettingOptions
{
    public class EmailSetting
    {
        public static string SectionName => "EmailSetting";
        public  string Mail { get; set; } = string.Empty;
        public  string DisplayName { get; set; } = string.Empty;
        public  string Host { get; set; } = string.Empty;
        public  string Password { get; set; } = string.Empty;
        public  int Port { get; set; }        
    }
}
