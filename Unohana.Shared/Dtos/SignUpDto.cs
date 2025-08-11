namespace Unohana.Shared.Dtos
{
    public class SignUpDto
    {
        public double IdentificationNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
