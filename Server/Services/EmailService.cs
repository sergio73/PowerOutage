using SendGrid.Helpers.Mail;
using SendGrid;
using Server.Dtos;

namespace Server.Services
{
    public interface IEmailService
    {
        Task<Response> SendEmail(EmailAddressDto from, EmailAddressDto to, string subject, string content, string? htmlContent = null);
    }

    public class EmailService : IEmailService
    {
        private readonly ISettings settings;

        public EmailService(ISettings settings) 
        {
            this.settings = settings;
        }

        public async Task<Response> SendEmail(EmailAddressDto from, EmailAddressDto to, string subject, string content, string? htmlContent = null)
        {
            var client = new SendGridClient(settings.EmailService.SendGridApiKey);
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
