using Unohana.Shared.Models;

namespace Unohana.Api.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<TeacherModel>> GetAll();
        Task<TeacherModel> GetById(string id);

        Task<bool> Add(TeacherModel teacher);
        Task<TeacherModel> Update(TeacherModel teacher);
        Task<bool> Remove(TeacherModel teacher);

        Task Save(TeacherModel teacher);
    }
}
