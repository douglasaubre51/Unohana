using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services.AuthenticationService
{
    public class SignInAuthentication
    {
        public async Task<bool> StudentSignIn(double IdentificationNumber, string Password)
        {
            string url = "https://localhost:7031/student/signin";
            HttpClient client = new HttpClient();
            SignInDto dto = new()
            {
                IdentificationNumber = IdentificationNumber,
                Password = Password
            };
            HttpResponseMessage response = await client.PostAsJsonAsync<SignInDto>(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"StudentSignIn failed!: {response.StatusCode}");
                return false;
            }
            // success
            return true;
        }
    }
}
