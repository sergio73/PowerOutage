namespace Server.Dtos
{
    public class SmsServiceDto
    {
        public TwilioDto? Twilio { get; set; }
        public string? PhoneNumberTo { get; set; }
        public string? PhoneNumberFrom { get; set; }
    }
}
