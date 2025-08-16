using MongoDB.Driver;
using Unohana.Api.Data;
using Unohana.Api.Models;

namespace Unohana.Api.Services.Otp
{
    public class SaveOtp(MongoDbContext context)
    {
        readonly IMongoCollection<OtpTemporaryCache> _collection = context.OtpTemporaryCache;

        public void Save(double identificationNumber, byte[] secretKey)
        {
            var filter = Builders<OtpTemporaryCache>.Filter.Eq(
                x => x.IdentificationNumber,
                identificationNumber
                );

            var update = Builders<OtpTemporaryCache>.Update.Set(
                x => x.SecretKey,
                secretKey
                );

            var result = _collection.UpdateOne(
                filter,
                update
                );

            // if otp doesnot exists in db
            long matchedCount = result.MatchedCount;
            if (matchedCount == 0)
            {
                OtpTemporaryCache cache = new()
                {
                    IdentificationNumber = identificationNumber,
                    SecretKey = secretKey
                };

                _collection.InsertOne(cache);
            }
        }
    }
}
