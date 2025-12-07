namespace Unohana.ViewModels.Channel;

public class ChannelManagerViewModel
{
    public List<ChannelDtos> Channels { get; set; } = [];

    [Required]
    public string ChannelTitleField { get; set; } = string.Empty;
}
