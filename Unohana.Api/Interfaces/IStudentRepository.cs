using Unohana.Shared.Models;

namespace Unohana.Api.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<StudentModel>> GetAll();
        Task<StudentModel> GetById(string id);

        Task Add(StudentModel student);
        Task<StudentModel> Update(StudentModel student);
        Task<bool> Remove(StudentModel student);

        Task Save(StudentModel student);
    }
}
