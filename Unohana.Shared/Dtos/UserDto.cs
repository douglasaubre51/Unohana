using MongoDB.Bson;

namespace Unohana.Shared.Dtos
{
    public class UserDto
    {
        public ObjectId Id { get; set; }
        public double IdentificationNumber { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
    }
}
