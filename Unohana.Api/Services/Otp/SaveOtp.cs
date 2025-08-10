using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Unohana.Api.Models;
using Unohana.Api.Models.ServiceSettings;

namespace Unohana.Api.Services.Otp
{
    public class SaveOtp
    {
        readonly IMongoCollection<OtpTemporaryCache> _collection;
        public SaveOtp(IOptions<MongoDbSettings> options)
        {
            MongoClient mongoClient = new(
                options.Value.ConnectionURI
                );
            IMongoDatabase database = mongoClient.GetDatabase(
                options.Value.DatabaseName
                );
            _collection = database.GetCollection<OtpTemporaryCache>(
                options.Value.OtpTemporaryCache
                );
        }
        public void Save(double identificationNumber, byte[] secretKey)
        {
            OtpTemporaryCache cache = new()
            {
                IdentificationNumber = identificationNumber,
                SecretKey = secretKey
            };
            var filter = Builders<OtpTemporaryCache>.Filter.Eq(
                x => x.IdentificationNumber,
                identificationNumber
                );
            var content = Builders<OtpTemporaryCache>.Update.Set(
                x => x.SecretKey,
                secretKey
                );
            var result = _collection.UpdateOne(
                filter,
                content
                );
            long matchedCount = result.MatchedCount;
            // if otp doesnot exists in db
            if (matchedCount == 0)
            {
                _collection.InsertOne(cache);
                return;
            }
        }
    }
}
