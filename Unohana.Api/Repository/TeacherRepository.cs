using Unohana.Api.Interfaces;
using Unohana.Shared.Models;

namespace Unohana.Api.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        public async Task<List<TeacherModel>> GetAll()
        {
            return null;
        }
        public async Task<TeacherModel> GetById(string id)
        {
            return null;
        }


        public async Task<bool> Add(TeacherModel teacher)
        {
            return true;
        }
        public async Task<TeacherModel> Update(TeacherModel teacher)
        {
            return null;
        }
        public async Task<bool> Remove(TeacherModel teacher)
        {
            return true;
        }

        public async Task Save(TeacherModel teacher)
        {
        }
    }
}
