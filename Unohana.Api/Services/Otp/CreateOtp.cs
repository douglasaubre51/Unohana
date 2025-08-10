using OtpNet;

namespace Unohana.Api.Services.Otp
{
    public class CreateOtp
    {
        public Totp Create(out byte[] secretKey, out string otp)
        {
            secretKey = Guid.NewGuid().ToByteArray();
            Totp? totp = new(secretKey, 120);
            otp = totp.ComputeTotp(DateTime.UtcNow);

            return totp;
        }
    }
}
