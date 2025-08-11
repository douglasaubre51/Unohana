using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OtpNet;
using Unohana.Api.Models;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Shared.Dtos;

namespace Unohana.Api.Services.Otp
{
    public class VerifyOtp
    {
        readonly IMongoCollection<OtpTemporaryCache> _collection;
        public VerifyOtp(IOptions<MongoDbSettings> options)
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
        public bool Verify(OtpVerifyDto dto)
        {
            var filter = Builders<OtpTemporaryCache>.Filter.Eq(
                x => x.IdentificationNumber,
                dto.IdentificationNumber
                );
            var storedOtpCache = _collection.Find(filter).FirstOrDefault();

            Totp totp = new Totp(storedOtpCache.SecretKey, 120);
            bool result = totp.VerifyTotp(dto.Otp, out long window);

            return result;
        }
    }
}
