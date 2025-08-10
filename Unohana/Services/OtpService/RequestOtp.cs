using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services.OtpService
{
    public class RequestOtp
    {
        public async Task Trigger(string Username, string Email)
        {
            try
            {
                string url = "https://localhost:44343/api/Otp/student/send-email";
                HttpClient client = new HttpClient();
                OtpInEmailDto dto = new()
                {
                    Username = Username,
                    Email = Email
                };
                HttpResponseMessage response = await client.PostAsJsonAsync<OtpInEmailDto>(url, dto);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"RequestOtp failed: {response.StatusCode}");
                }
                // success
                Debug.WriteLine($"RequestOtp requested an email!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RequestOtp error: {ex}");
            }
        }
    }
}
