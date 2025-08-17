using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services
{
    public class OtpService(HttpClient client)
    {
        readonly HttpClient _client = client;

        // teacher
        public async Task RequestOtpForTeacher(
            string Username,
            string Email,
            double EmployeeId
            )
        {
            OtpInEmailDto dto = new()
            {
                Username = Username,
                Email = Email,
                IdentificationNumber = EmployeeId
            };
            string url = "api/Otp/teacher/send-email";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"RequestOtp failed: {response.StatusCode}");
            }
            // success
            Debug.WriteLine($"RequestOtp requested an email!");
        }

        public async Task<bool> RequestVerificationForTeacher(
            string Otp,
            double IdentificationNumber
            )
        {
            OtpVerifyDto dto = new()
            {
                Otp = Otp,
                IdentificationNumber = IdentificationNumber
            };
            string url = "api/Otp/teacher/verify-otp";
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

        // student
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
                IdentificationNumber = RegisterNumber
            };
            string url = "api/Otp/student/send-email";
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
            string url = "api/Otp/student/verify-otp";
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
