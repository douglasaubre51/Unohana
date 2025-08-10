namespace Unohana.Shared.Dtos
{
    public class OtpVerifyDto
    {
        public double IdentificationNumber { get; set; }
        public string Otp { get; set; } = string.Empty;
    }
}
