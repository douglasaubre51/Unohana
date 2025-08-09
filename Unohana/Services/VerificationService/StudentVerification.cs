using System.Diagnostics;

namespace Unohana.Services.VerificationService
{
    public class StudentVerification
    {
        public async Task<bool> Verify(double regno)
        {
            try
            {
                Debug.WriteLine($"reg no: {regno}");
                string url = "https://localhost:44343/api/Verification/student/" + regno;
                HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"register no doesnot exist!: {response.StatusCode}");
                    return false;
                }
                // success
                Debug.WriteLine("register no exists!");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"student verification error!: {ex}");
                return false;
            }
        }
    }
}
