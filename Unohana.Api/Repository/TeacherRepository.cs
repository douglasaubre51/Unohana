using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Shared.Models;

namespace Unohana.Api.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        readonly IMongoCollection<TeacherModel> collection;

        public TeacherRepository(IOptions<MongoDbSettings> options)
        {
            MongoClient client = new(
                options.Value.ConnectionURI
                );

            IMongoDatabase db = client.GetDatabase(
                options.Value.DatabaseName
                );

            collection = db.GetCollection<TeacherModel>(
                options.Value.TeacherCollection
                );
        }

        public async Task<List<TeacherModel>> GetAll()
        {
            var filter = Builders<TeacherModel>.Filter.Empty;
            return await collection.Find(filter).ToListAsync();
        }
        public async Task<TeacherModel> GetById(string id)
        {
            var filter = Builders<TeacherModel>.Filter.Eq(
                x => x.Id,
                id
                );
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task Add(TeacherModel teacher)
        {
            await collection.InsertOneAsync(teacher);
        }
        public async Task Update(TeacherModel teacher)
        {
            var filter = Builders<TeacherModel>.Filter.Eq(
                x => x.Id,
                teacher.Id
                );

            var update = Builders<TeacherModel>.Update.Set(
                x => x,
                teacher
                );

            await collection.UpdateOneAsync(filter, update);
        }
        public async Task Remove(TeacherModel teacher)
        {
            var filter = Builders<TeacherModel>.Filter.Eq(
                x => x.Id,
                teacher.Id
                );

            await collection.DeleteOneAsync(
                filter
                );
        }
    }
}
