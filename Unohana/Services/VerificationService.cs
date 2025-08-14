using System.Diagnostics;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Services
{
    public class VerificationService(HttpClient client)
    {
        readonly HttpClient _client = client;

        public async Task<StudentCSVModel?> VerifyStudent(double regno)
        {
            string url = "https://localhost:7031/api/Verification/student/" + regno;
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"register no doesnot exist!: {response.StatusCode}");
                return null;
            }
            // success
            StudentCSVModel? studentCSVModel = await response.Content.ReadFromJsonAsync<StudentCSVModel>();
            return studentCSVModel;
        }
    }
}
