namespace Server.Dtos
{
    public class EmailAddressDto
    {
        public EmailAddressDto(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
