using MongoDB.Bson;

namespace Unohana.Api.Models
{
    public class OtpTemporaryCache
    {
        public ObjectId Id { get; set; }
        public byte[]? SecretKey { get; set; }
        public double IdentificationNumber { get; set; }
    }
}
