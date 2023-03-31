namespace Server.Services
{
    public interface INotificationService
    {
        Task NotifyPowerOutage();
        Task NotifyPowerRestore();
    }

    public class NotificationService : INotificationService
    {
        private readonly ISettings _settings;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IStatusService _statusService;

        public NotificationService(ISettings settings, IEmailService emailService, ISmsService smsService, IStatusService statusService)
        {
            _settings = settings;
            _emailService = emailService;
            _smsService = smsService;
            _statusService = statusService;
        }

        public async Task NotifyPowerOutage()
        {
            await SendNotification("Power outage!", "There was a power outage recorded at " + DateTime.Now);
        }

        public async Task NotifyPowerRestore()
        {
            var status = _statusService.GetStatus();
            var timeOffline = DateTime.Now - status.LastTimeOffline;

            await SendNotification("Power restored!", "Power was restored, time offline: " + timeOffline);
        }

        private async Task SendNotification(string header, string content)
        {
            var emailResponse = _emailService.IsEnabled ? _emailService.SendEmail(_settings.EmailService.From, _settings.EmailService.To, header, content) : Task.CompletedTask;
            var smsResponse = _smsService.IsEnabled ? _smsService.SendSms(_settings.SmsService.PhoneNumberFrom, _settings.SmsService.PhoneNumberTo, content) : Task.CompletedTask;

            await Task.WhenAll(emailResponse, smsResponse);
        }
    }
}
