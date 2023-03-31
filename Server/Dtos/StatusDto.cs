namespace Server.Dtos
{
    public class StatusDto
    {
        public DateTime? LastTimeOnline { get; set; }
        public DateTime? LastTimeOffline { get; set; }
        public bool UserAlerted { get; set; }
    }
}
