using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Unohana.Shared.Models
{
    public class TeacherCsvModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public double EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
    }
}
