using Server.Dtos;

namespace Server
{
    public interface ISettings
    {
        EmailServiceDto EmailService { get; }
        SmsServiceDto SmsService { get; }
        string AliveFileName { get; }
    }
    public class Settings : ISettings
    {
        public Settings(IConfiguration configuration)
        {
            EmailService = new EmailServiceDto(
                new EmailAddressDto(configuration.GetValue<string>("EmailService:From:Name"), configuration.GetValue<string>("EmailService:From:Email")),
                new EmailAddressDto(configuration.GetValue<string>("EmailService:To:Name"), configuration.GetValue<string>("EmailService:To:Email")),
                configuration.GetValue<string>("EmailService:SendGrid:ApiKey")
            );
            SmsService = new SmsServiceDto(
                new TwilioDto(configuration.GetValue<string>("SmsService:Twilio:SID"), configuration.GetValue<string>("SmsService:Twilio:Secret")),
                configuration.GetValue<string>("SmsService:PhoneNumberFrom"),
                configuration.GetValue<string>("SmsService:PhoneNumberTo")
            );
            AliveFileName = configuration.GetValue<string>("AliveFileName");
        }

        public EmailServiceDto EmailService { get; }
        public SmsServiceDto SmsService { get; }
        public string AliveFileName { get; }
    }
}
