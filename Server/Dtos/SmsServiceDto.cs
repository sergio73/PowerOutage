namespace Server.Dtos
{
    public class SmsServiceDto
    {
        public SmsServiceDto(TwilioDto twilio, string phoneNumberFrom, string phoneNumberTo)
        {
            Twilio = twilio;
            PhoneNumberFrom = phoneNumberFrom;
            PhoneNumberTo = phoneNumberTo;
        }

        public TwilioDto Twilio { get; }
        public string PhoneNumberTo { get; }
        public string PhoneNumberFrom { get; }
    }
}
