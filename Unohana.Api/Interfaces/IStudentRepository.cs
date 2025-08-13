using Unohana.Shared.Models;

namespace Unohana.Api.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<StudentModel>> GetAll();
        Task<StudentModel> GetById(string id);
        Task<StudentModel> GetByRegisterNumber(double id);

        Task Add(StudentModel student);
        Task Update(StudentModel student);
        Task Remove(StudentModel student);
    }
}
