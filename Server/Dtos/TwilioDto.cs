namespace Server.Dtos
{
    public class TwilioDto
    {
        public TwilioDto(string twilioSID, string twilioSecret)
        {
            TwilioSID = twilioSID;
            TwilioSecret = twilioSecret;
        }

        public string TwilioSID { get; }
        public string TwilioSecret { get; }
    }
}
