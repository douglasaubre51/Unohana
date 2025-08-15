using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Repository
{
    public class TeacherInfoRepository : ITeacherInfoRepository
    {
        readonly IMongoCollection<TeacherCSVModel> _collection;

        public TeacherInfoRepository(IOptions<MongoDbSettings> options)
        {
            MongoClient client = new(
                options.Value.ConnectionURI
                );

            IMongoDatabase db = client.GetDatabase(
                options.Value.DatabaseName
                );

            _collection = db.GetCollection<TeacherCSVModel>(
                options.Value.TeacherInfoCollection
                );
        }

        public async Task<TeacherCSVModel> GetByEmployeeId(double employeeId)
        {
            var filter = Builders<TeacherCSVModel>.Filter.Eq(
                x => x.EmployeeId,
                employeeId
                );

            return await _collection.Find<TeacherCSVModel>(filter).FirstOrDefaultAsync();
        }
    }
}
