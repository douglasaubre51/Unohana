using OtpNet;

namespace Unohana.Api.Services.Otp
{
    public class CreateOtp
    {
        public Totp Create(out byte[] secretKey, out string otp)
        {
            secretKey = Guid.NewGuid().ToByteArray();

            // otp object for verification!
            Totp? totp = new(secretKey, 120);

            // real otp!
            otp = totp.ComputeTotp(DateTime.UtcNow);

            return totp;
        }
    }
}
