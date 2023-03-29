using SendGrid;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Server.Services
{
    public interface ISmsService
    {
        Task<string> SendSms(string phoneNumberFrom, string phoneNumberTo, string content);
    }

    public class SmsService : ISmsService
    {
        private readonly ISettings settings;

        public SmsService(ISettings settings) 
        {
            this.settings = settings;
        }

        public async Task<string> SendSms(string phoneNumberFrom, string phoneNumberTo, string content)
        {
            TwilioClient.Init(settings.SmsService.Twilio.TwilioSID, settings.SmsService.Twilio.TwilioSecret);

            var message = await MessageResource.CreateAsync(
                body: content,
                from: new PhoneNumber(phoneNumberFrom),
                to: new PhoneNumber(phoneNumberTo)
            );

            return message.Status.ToString();
        }
    }
}
