using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Services.Authentication
{
    public class Teacher(HttpClient client)
    {
        readonly HttpClient _client = client;

        public async Task<bool> SignUp(
            double employeeId,
            string username,
            string email,
            string password
            )
        {
            SignUpDto dto = new()
            {
                IdentificationNumber = employeeId,
                Username = username,
                Email = email,
                Password = password
            };
            string url = "https://localhost:7031/api/Auth/teacher/signup";
            HttpResponseMessage response = await _client.PostAsJsonAsync<SignUpDto>(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("couldn't create teacher account!" + response.StatusCode);
                return false;
            }
            // success
            return true;
        }
    }
}
