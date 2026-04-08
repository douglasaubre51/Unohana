using Microsoft.AspNetCore.SignalR;
using Unohana.Storage;

namespace Unohana.Hubs;

public class ChatHub(
    ChannelRepository channelRepo,
    TutorRepository tutorRepo
) : Hub
{
    private readonly ChannelRepository _channelRepo = channelRepo;
    private readonly TutorRepository _tutorRepo = tutorRepo;

    public async Task BroadcastMessage(ChatDtos dto)
    {
        Console.WriteLine("Broadcasting...");
        Console.WriteLine(dto.ChannelId);
        Console.WriteLine(dto.UserId);
        Console.WriteLine(dto.Text);

        var dbTutor = _tutorRepo.GetById(int.Parse(dto.UserId));
        var dbChannel = _channelRepo.GetById(int.Parse(dto.ChannelId));
        dbChannel!.Messages!.Add(new Message()
        {
            Text = dto.Text,
            CreatedAt = DateTime.UtcNow.ToLocalTime(),
            Tutor = dbTutor!,
            Channel = dbChannel
        });
        _channelRepo.Save();

        await Clients.Group(dto.ChannelId).SendAsync("UpdateMessage", dto);
    }

    public async Task AddToGroup(string name, string connId)
        => await Groups.AddToGroupAsync(connId, name);
    public async Task RemoveFromGroup(string name, string connId)
        => await Groups.RemoveFromGroupAsync(connId, name);

    public async Task SetUserId(ChatHubDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.ChannelId)) return;

        ConnectedUserStore.AddUser(Context.ConnectionId, dto.UserId);
        await AddToGroup(dto.ChannelId, Context.ConnectionId);

        Console.WriteLine($"user connected: {ConnectedUserStore.GetUserId(Context.ConnectionId)}");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"user disconnected: {ConnectedUserStore.GetUserId(Context.ConnectionId)}");
        ConnectedUserStore.RemoveUser(Context.ConnectionId);
    }
}