using Unohana.Api.Interfaces;
using Unohana.Shared.Dtos;
using Unohana.Shared.Models;

namespace Unohana.Api.Services.Authentication
{
    public class CreateStudentAccount
    {
        readonly IStudentRepository _repository;
        public CreateStudentAccount(IStudentRepository repository)
        {
            _repository = repository;
        }
        public async Task Create(SignUpDto dto)
        {
            StudentModel model = new StudentModel()
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                RegisterNumber = dto.IdentificationNumber,
            };
            await _repository.Add(model);
        }
    }
}
