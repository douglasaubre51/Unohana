using Unohana.Shared.Models;

namespace Unohana.Api.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<TeacherModel>> GetAll();
        Task<TeacherModel> GetById(string id);

        Task Add(TeacherModel teacher);
        Task Update(TeacherModel teacher);
        Task Remove(TeacherModel teacher);
    }
}
