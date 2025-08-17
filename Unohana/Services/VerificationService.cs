using System.Diagnostics;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Services
{
    public class VerificationService(HttpClient client)
    {
        readonly HttpClient _client = client;

        public async Task<TeacherCSVModel?> VerifyTeacher(double employeeId)
        {
            string url = "api/Verification/teacher/" + employeeId;
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"employee id does'nt exist! : {response.StatusCode}");
                return null;
            }
            // success
            TeacherCSVModel? teacher = await response.Content.ReadFromJsonAsync<TeacherCSVModel>();
            return teacher;
        }

        public async Task<StudentCSVModel?> VerifyStudent(double regno)
        {
            string url = "api/Verification/student/" + regno;
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
