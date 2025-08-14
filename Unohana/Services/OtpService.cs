using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services
{
    public class OtpService(HttpClient client)
    {
        readonly HttpClient _client = client;

        public async Task RequestOtp(
            string Username,
            string Email,
            double RegisterNumber
            )
        {
            OtpInEmailDto dto = new()
            {
                Username = Username,
                Email = Email,
                RegisterNumber = RegisterNumber
            };
            string url = "https://localhost:7031/api/Otp/student/send-email";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"RequestOtp failed: {response.StatusCode}");
            }
            // success
            Debug.WriteLine($"RequestOtp requested an email!");
        }
        public async Task<bool> RequestVerification(
            string Otp,
            double IdentificationNumber
            )
        {
            OtpVerifyDto dto = new()
            {
                Otp = Otp,
                IdentificationNumber = IdentificationNumber
            };
            string url = "https://localhost:7031/api/Otp/student/verify-otp";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"invalid otp: {response.StatusCode}");
                return false;
            }
            // success
            Debug.WriteLine($"otp is valid!");
            return true;
        }
    }
}
