namespace Unohana.Dtos;

public class ChatDtos
{
    public string UserId { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public class ChatHubDto
{
    public string UserId { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;
}
