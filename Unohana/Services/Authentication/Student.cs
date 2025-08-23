using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services.Authentication
{
    public class Student(HttpClient client)
    {
        readonly HttpClient _client = client;

        public async Task<bool> SignUp(
            string password,
            double identificationNumber,
            string username,
            string email
            )
        {
            SignUpDto dto = new()
            {
                Password = password,
                IdentificationNumber = identificationNumber,
                Email = email,
                Username = username
            };
            string url = "api/Auth/student/signup";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"couldn't sign up! : {response.StatusCode}");
                return false;
            }
            // success
            return true;
        }
        public async Task<bool> SignIn(double IdentificationNumber, string Password)
        {
            SignInDto dto = new()
            {
                IdentificationNumber = IdentificationNumber,
                Password = Password
            };
            string url = "api/Auth/student/signin";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, dto);
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
