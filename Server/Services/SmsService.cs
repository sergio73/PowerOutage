using SendGrid;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using System.Runtime;
using System.Configuration;

namespace Server.Services
{
    public interface ISmsService : IIsEnabled
    {
        Task<string> SendSms(string phoneNumberFrom, string phoneNumberTo, string content);
    }

    public class SmsService : ISmsService
    {
        private readonly ISettings _settings;

        public SmsService(ISettings settings) 
        {
            _settings = settings;
        }

        public bool IsEnabled => !string.IsNullOrWhiteSpace(_settings.SmsService?.Twilio?.SID) && !string.IsNullOrWhiteSpace(_settings.SmsService?.Twilio?.Secret);

        public async Task<string> SendSms(string phoneNumberFrom, string phoneNumberTo, string content)
        {
            if (!IsEnabled)
            {
                throw new ConfigurationErrorsException("SMS service cannot be used because is not configured");
            }

            TwilioClient.Init(_settings.SmsService.Twilio.SID, _settings.SmsService.Twilio.Secret);

            var message = await MessageResource.CreateAsync(
                body: content,
                from: new PhoneNumber(phoneNumberFrom),
                to: new PhoneNumber(phoneNumberTo)
            );

            return message.Status.ToString();
        }
    }
}
