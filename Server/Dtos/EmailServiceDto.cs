namespace Server.Dtos
{
    public class EmailServiceDto
    {
        public EmailServiceDto(EmailAddressDto from, EmailAddressDto to, string sendGridApiKey)
        {
            From = from;
            To = to;
            SendGridApiKey = sendGridApiKey;
        }

        public EmailAddressDto From;
        public EmailAddressDto To;
        public string SendGridApiKey;
    }
}
