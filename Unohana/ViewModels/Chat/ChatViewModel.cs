namespace Unohana.ViewModels.Chat;

public class ChatViewModel
{
    public List<Models.Channel>? Channels { get; set; }
    public Models.Channel? CurrentChannel { get; set; }

    public string Message { get; set; } = string.Empty;
}
