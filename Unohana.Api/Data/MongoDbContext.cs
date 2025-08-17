using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Unohana.Api.Models;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Shared.Models;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Data
{
    public class MongoDbContext
    {
        readonly IMongoDatabase _db;
        readonly IOptions<MongoDbSettings> _options;

        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            _options = options;
            MongoClient client = new(
               _options.Value.ConnectionURI
               );

            _db = client.GetDatabase(
                _options.Value.DatabaseName
               );
        }

        // get collections
        public IMongoCollection<StudentModel> StudentCollection
            => _db.GetCollection<StudentModel>(
                _options.Value.StudentCollection
                );

        public IMongoCollection<TeacherModel> TeacherCollection
            => _db.GetCollection<TeacherModel>(
                _options.Value.TeacherCollection
                );

        public IMongoCollection<MessageModel> MessageCollection
            => _db.GetCollection<MessageModel>(
                _options.Value.MessageCollection
                );

        public IMongoCollection<ChannelModel> ChannelCollection
            => _db.GetCollection<ChannelModel>(
                _options.Value.ChannelCollection
                );

        // get seed collections
        public IMongoCollection<StudentCSVModel> StudentInfoCollection
            => _db.GetCollection<StudentCSVModel>(
                _options.Value.StudentInfoCollection
                );

        public IMongoCollection<TeacherCSVModel> TeacherInfoCollection
            => _db.GetCollection<TeacherCSVModel>(
                _options.Value.TeacherInfoCollection
                );

        // otp temp cache
        public IMongoCollection<OtpTemporaryCache> OtpTemporaryCache
            => _db.GetCollection<OtpTemporaryCache>(
                _options.Value.OtpTemporaryCache
                );
    }
}
