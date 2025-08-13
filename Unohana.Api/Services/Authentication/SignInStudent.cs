using Unohana.Api.Interfaces;
using Unohana.Shared.Dtos;
using Unohana.Shared.Models;

namespace Unohana.Api.Services.Authentication
{
    public class SignInStudent
    {
        readonly IStudentRepository _repository;
        public SignInStudent(IStudentRepository repository)
        {
            _repository = repository;
        }
        public async Task<StudentModel> SignIn(SignInDto dto)
        {
            StudentModel model = await _repository.GetByRegisterNumber(dto.IdentificationNumber);
            return model;
        }
    }
}
