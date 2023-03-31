using Server.Dtos;
using Server.Services;

namespace Server
{
    public interface ISettings
    {
        EmailServiceDto EmailService { get; }
        SmsServiceDto SmsService { get; }
        string StatusFileName { get; }
        int TimeToAlert { get; }
    }

    public class Settings : ISettings
    {
        public Settings(IConfiguration configuration)
        {
            EmailService = configuration.GetSection("EmailService").Get<EmailServiceDto>();
            SmsService = configuration.GetSection("SmsService").Get<SmsServiceDto>();

            StatusFileName = configuration.GetValue<string>("StatusFileName");
            TimeToAlert = configuration.GetValue<int>("TimeToAlert");
        }

        public EmailServiceDto EmailService { get; }
        public SmsServiceDto SmsService { get; }
        public string StatusFileName { get; }
        public int TimeToAlert { get; }
    }
}
