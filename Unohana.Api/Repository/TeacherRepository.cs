using MongoDB.Driver;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Shared.Models;

namespace Unohana.Api.Repository
{
    public class TeacherRepository(MongoDbContext context) : ITeacherRepository
    {
        readonly IMongoCollection<TeacherModel> collection = context.TeacherCollection;

        public async Task<List<TeacherModel>> GetAll()
            => await collection
            .Find(
                x => true
                )
            .ToListAsync();

        public async Task<TeacherModel> GetById(string id)
            => await collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        public async Task<TeacherModel> GetByEmployeeId(double employeeId)
            => await collection
            .Find(e => e.EmployeeId == employeeId)
            .FirstOrDefaultAsync();

        public async Task Add(TeacherModel teacher)
            => await collection.InsertOneAsync(teacher);

        public async Task Update(TeacherModel teacher)
            => await collection.ReplaceOneAsync(
                x => x.EmployeeId == teacher.EmployeeId,
                teacher
                );

        public async Task Remove(TeacherModel teacher)
            => await collection.DeleteOneAsync(
                teacher.Id
                );
    }
}
