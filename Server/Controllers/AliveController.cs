using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.IO;
using System.Data;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliveController : ControllerBase
    {
        private string _apiKey;
        private string _aliveFileName;

        public AliveController() 
        {
            _apiKey = Environment.GetEnvironmentVariable("SendGridApiKey") ?? throw new NoNullAllowedException("'SendGridApiKey' environment variable cannot be null");
            _aliveFileName = Environment.GetEnvironmentVariable("AliveFileName") ?? throw new NoNullAllowedException("'AliveFileName' environment variable cannot be null"); ;
        }

        [HttpPost]
        public Task<string> Post()
        {
            return Task.FromResult("test");
        }

        private async Task<Response> SendNotificationEmail()
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("homeassistant@sergiodb.com", "Example User");
            var subject = "Sending with Twilio SendGrid is Fun";
            var to = new EmailAddress("sergio.mzn73@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
