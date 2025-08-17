using MongoDB.Driver;
using OtpNet;
using Unohana.Api.Data;
using Unohana.Api.Models;
using Unohana.Shared.Dtos;

namespace Unohana.Api.Services.Otp
{
    public class VerifyOtp(MongoDbContext context)
    {
        readonly IMongoCollection<OtpTemporaryCache> _collection = context.OtpTemporaryCache;

        public bool Verify(OtpVerifyDto dto)
        {
            var storedOtpCache = _collection
                .Find(
                x => x.IdentificationNumber == dto.IdentificationNumber
                )
                .FirstOrDefault();

            Totp totp = new(storedOtpCache.SecretKey, 120);
            bool result = totp.VerifyTotp(dto.Otp, out long window);

            return result;
        }
    }
}
