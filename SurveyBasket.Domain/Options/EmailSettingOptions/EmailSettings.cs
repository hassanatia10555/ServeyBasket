namespace SurveyBasket.Domain.Options.EmailSettingOptions
{
    public class EmailSettings
    {
        public static string SectionName => "EmailSetting-1";
        public  string Mail { get; set; } = string.Empty;
        public  string DisplayName { get; set; } = string.Empty;
        public  string Host { get; set; } = string.Empty;
        public  string Password { get; set; } = string.Empty;
        public  int Port { get; set; }        
    }
}
