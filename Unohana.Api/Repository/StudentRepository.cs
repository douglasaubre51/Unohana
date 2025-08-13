using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Shared.Models;

namespace Unohana.Api.Repository
{
    public class StudentRepository : IStudentRepository
    {
        readonly IMongoCollection<StudentModel> dbCollection;
        public StudentRepository(IOptions<MongoDbSettings> options)
        {
            MongoClient mongoClient = new MongoClient(options.Value.ConnectionURI);
            IMongoDatabase? mongoDb = mongoClient.GetDatabase(options.Value.DatabaseName);
            dbCollection = mongoDb.GetCollection<StudentModel>(options.Value.StudentCollection);
        }
        public async Task<List<StudentModel>> GetAll()
        {
            return await dbCollection.Find(_ => true).ToListAsync();
        }
        public async Task<StudentModel> GetById(string id)
        {
            return await dbCollection.Find(x => x.Id == id).SingleOrDefaultAsync();
        }
        public async Task<StudentModel> GetByRegisterNumber(double registerNo)
        {
            return await dbCollection.Find(x => x.RegisterNumber == registerNo).SingleOrDefaultAsync();
        }
        public async Task Add(StudentModel student)
        {
            await dbCollection.InsertOneAsync(student);
        }
        public async Task Update(StudentModel student)
        {
            var filter = Builders<StudentModel>.Filter.Eq(
                x => x.Id,
                student.Id
                );
            var updatedModel = Builders<StudentModel>.Update.Set(
                x => x,
                student
                );
            await dbCollection.UpdateOneAsync(
               filter,
               updatedModel
               );
        }
        public async Task Remove(StudentModel student)
        {
            var filter = Builders<StudentModel>.Filter.Eq(
                x => x.Id,
                student.Id
                );
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
