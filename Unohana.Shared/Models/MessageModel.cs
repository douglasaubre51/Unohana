using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Unohana.Shared.Models
{
    public class MessageModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string SenderId { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
