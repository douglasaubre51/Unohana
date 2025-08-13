using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services.OtpService
{
    public class RequestOtpVerification
    {
        public async Task<bool> Trigger(string Otp, double IdentificationNumber)
        {
            try
            {
                string url = "https://localhost:7031/api/Otp/student/verify-otp";
                HttpClient client = new HttpClient();
                OtpVerifyDto dto = new()
                {
                    Otp = Otp,
                    IdentificationNumber = IdentificationNumber
                };
                HttpResponseMessage response = await client.PostAsJsonAsync<OtpVerifyDto>(url, dto);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"invalid otp: {response.StatusCode}");
                    return false;
                }
                // success
                Debug.WriteLine($"otp is valid!");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RequestOtpVerification error: {ex}");
                return false;
            }
        }
    }
}
