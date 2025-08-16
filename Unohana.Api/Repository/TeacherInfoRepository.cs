using MongoDB.Driver;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Repository
{
    public class TeacherInfoRepository(MongoDbContext context) : ITeacherInfoRepository
    {
        readonly IMongoCollection<TeacherCSVModel> _collection = context.TeacherInfoCollection;

        public async Task<TeacherCSVModel> GetByEmployeeId(double employeeId)
            => await _collection
            .Find<TeacherCSVModel>(
                x => x.EmployeeId == employeeId
                )
            .FirstOrDefaultAsync();
    }
}
