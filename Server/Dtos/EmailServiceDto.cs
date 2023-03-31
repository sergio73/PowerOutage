using System.Security.Policy;

namespace Server.Dtos
{
    public class EmailServiceDto
    {
        public EmailAddressDto? From { get; set; }
        public EmailAddressDto? To { get; set; }
        public string? SendGridApiKey { get; set; }
    }
}
