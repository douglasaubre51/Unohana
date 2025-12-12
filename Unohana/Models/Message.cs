namespace Unohana.Models;

public class Message
{
    [Key]
    public int Id { get; set; }
    public required Tutor Tutor { get; set; }
    public int ChannelId { get; set; }
    public Channel? Channel { get; set; }

    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
