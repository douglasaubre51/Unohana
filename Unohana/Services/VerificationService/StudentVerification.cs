using System.Diagnostics;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Services.VerificationService
{
    public class StudentVerification
    {
        public async Task<StudentCSVModel?> Verify(double regno)
        {
            try
            {
                Debug.WriteLine($"reg no: {regno}");
                string url = "https://localhost:7031/api/Verification/student/" + regno;
                HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"register no doesnot exist!: {response.StatusCode}");
                    return null;
                }
                // success
                Debug.WriteLine("register no exists!");
                StudentCSVModel? studentCSVModel = await response.Content.ReadFromJsonAsync<StudentCSVModel>();
                return studentCSVModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"student verification error!: {ex}");
                return null;
            }
        }
    }
}
