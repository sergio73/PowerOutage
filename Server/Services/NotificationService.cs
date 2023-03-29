namespace Server.Services
{
    public interface INotificationService
    {
        Task NotifyPowerOutage();
    }

    public class NotificationService : INotificationService
    {
        private readonly ISettings _settings;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public NotificationService(ISettings settings, IEmailService emailService, ISmsService smsService)
        {
            _settings = settings;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task NotifyPowerOutage()
        {
            string content = "There was a power outage recorded at " + DateTime.Now;

            var emailResponse = _emailService.SendEmail(_settings.EmailService.From, _settings.EmailService.To, "Power Outage!", content);
            var smsResponse = _smsService.SendSms(_settings.SmsService.PhoneNumberFrom, _settings.SmsService.PhoneNumberTo, content);

            await Task.WhenAll(emailResponse, smsResponse);
        }

        public async Task NotifyPowerRestore()
        {
            string content = "Power was restored at " + DateTime.Now;

            var emailResponse = _emailService.SendEmail(_settings.EmailService.From, _settings.EmailService.To, "Power restored!", content);
            var smsResponse = _smsService.SendSms(_settings.SmsService.PhoneNumberFrom, _settings.SmsService.PhoneNumberTo, content);

            await Task.WhenAll(emailResponse, smsResponse);
        }
    }
}
