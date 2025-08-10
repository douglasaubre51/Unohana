using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Repository
{
    public class StudentInfoRepository : IStudentInfoRepository
    {
        readonly IMongoCollection<StudentCSVModel> _collection;
        public StudentInfoRepository(IOptions<MongoDbSettings> options)
        {
            var mongoClient = new MongoClient(options?.Value.ConnectionURI);
            var database = mongoClient.GetDatabase(options?.Value.DatabaseName);
            _collection = database.GetCollection<StudentCSVModel>(options?.Value.StudentInfoCollection);
        }
        public async Task<StudentCSVModel> GetByRegisterNumber(double registerNumber)
        {
            var filter = Builders<StudentCSVModel>.Filter.Eq(
                s => s.RegisterNumber,
                registerNumber
                );
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
