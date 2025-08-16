using MongoDB.Driver;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Shared.Models;

namespace Unohana.Api.Repository
{
    public class StudentRepository(MongoDbContext context) : IStudentRepository
    {
        readonly IMongoCollection<StudentModel> dbCollection = context.StudentCollection;

        public async Task<List<StudentModel>> GetAll()
            => await dbCollection
            .Find(_ => true)
            .ToListAsync();

        public async Task<StudentModel> GetById(string id)
            => await dbCollection
            .Find(x => x.Id == id)
            .SingleOrDefaultAsync();

        public async Task<StudentModel> GetByRegisterNumber(double registerNo)
            => await dbCollection
            .Find(x => x.RegisterNumber == registerNo)
            .SingleOrDefaultAsync();

        public async Task Add(StudentModel student)
            => await dbCollection.InsertOneAsync(student);

        public async Task Update(StudentModel student)
            => await dbCollection
            .ReplaceOneAsync(
               x => x.RegisterNumber == student.RegisterNumber,
               student
               );

        public async Task Remove(StudentModel student)
           => await dbCollection.DeleteOneAsync(
               x => x.RegisterNumber == student.RegisterNumber
               );
    }
}
