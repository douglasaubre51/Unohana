namespace Unohana.Shared.Models
{
    public class MessageModel
    {
        public string MessageId { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
