using MongoDB.Driver;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Repository
{
    public class StudentInfoRepository(MongoDbContext context) : IStudentInfoRepository
    {
        readonly IMongoCollection<StudentCSVModel> _collection = context.StudentInfoCollection;

        public async Task<StudentCSVModel> GetByRegisterNumber(double registerNumber)
            => await _collection
                .Find(x => x.RegisterNumber == registerNumber)
                .FirstOrDefaultAsync();
    }
}
