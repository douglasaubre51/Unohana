using OtpNet;

namespace Unohana.Api.Services.Otp
{
    public class CreateOtp
    {
        public string Create()
        {
            byte[] secretkey = Guid.NewGuid().ToByteArray();
            Totp? TOTP = new(secretkey, 120);
            string otp = TOTP.ComputeTotp(DateTime.UtcNow);
            return otp;
        }
    }
}
