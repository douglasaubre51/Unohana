using Unohana.Api.Interfaces;
using Unohana.Shared.Dtos;
using Unohana.Shared.Models;

namespace Unohana.Api.Services.Authentication
{
    public class StudentAuthentication
    {
        readonly IStudentRepository _repository;
        public StudentAuthentication(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task SignIn(SignInDto dto)
        {

        }

        public async Task SignUp(SignUpDto dto)
        {
            StudentModel model = new()
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                Profile = dto.Profile
            };

            await _repository.Add(model);
        }
    }
}
