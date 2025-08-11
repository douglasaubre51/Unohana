using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services.AuthenticationService
{
    public class SignUpAuthentication
    {
        public async Task<bool> StudentSignUp(
            string password,
            double identificationNumber,
            string username,
            string email
            )
        {
            try
            {
                string url = "https://localhost:44343/api/Auth/student/signup";
                HttpClient client = new HttpClient();
                SignUpDto dto = new()
                {
                    Password = password,
                    IdentificationNumber = identificationNumber,
                    Email = email,
                    Username = username
                };
                HttpResponseMessage response = await client.PostAsJsonAsync<SignUpDto>(url, dto);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"couldn't sign up! : {response.StatusCode}");
                    return false;
                }
                // success
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SignUpAuthentication error: {ex}");
                return false;
            }
        }
    }
}
