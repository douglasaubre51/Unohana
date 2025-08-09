namespace Unohana.Api.Models.ServiceSettings
{
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;


        public string StudentCollection { get; set; } = string.Empty;
        public string TeacherCollection { get; set; } = string.Empty;

        public string ChannelCollection { get; set; } = string.Empty;
        public string MessageCollection { get; set; } = string.Empty;
    }
}
