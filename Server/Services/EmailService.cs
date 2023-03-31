using SendGrid.Helpers.Mail;
using SendGrid;
using Server.Dtos;
using System.Configuration;

namespace Server.Services
{
    public interface IEmailService : IIsEnabled
    {
        Task<Response> SendEmail(EmailAddressDto from, EmailAddressDto to, string subject, string content, string? htmlContent = null);
    }

    public class EmailService : IEmailService
    {
        private readonly ISettings _settings;

        public EmailService(ISettings settings) 
        {
            _settings = settings;
        }

        public bool IsEnabled => !string.IsNullOrWhiteSpace(_settings.EmailService?.SendGridApiKey);

        public async Task<Response> SendEmail(EmailAddressDto from, EmailAddressDto to, string subject, string content, string? htmlContent = null)
        {
            if (!IsEnabled)
            {
                throw new ConfigurationErrorsException("Email service cannot be used because is not configured");
            }

            var client = new SendGridClient(_settings.EmailService.SendGridApiKey);
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(from.Email, from.Name), 
                new EmailAddress(to.Email, to.Name), 
                subject, 
                content, 
                htmlContent ?? content);

            return await client.SendEmailAsync(msg);
        }
    }
}
