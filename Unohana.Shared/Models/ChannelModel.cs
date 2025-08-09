using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Unohana.Shared.Models
{
    public class ChannelModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;

        public List<MessageModel>? MessageList { get; set; }
        public List<MessageModel>? PinnedMessageList { get; set; }

        public List<StudentModel>? StudentList { get; set; }
        public List<TeacherModel>? TeacherList { get; set; }
    }
}
