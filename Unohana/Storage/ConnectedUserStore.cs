namespace Unohana.Storage;

public static class ConnectedUserStore
{
    private static Dictionary<string, string> Users = [];
    private static Dictionary<string, string> Channels = [];

    public static void AddChannel(string userId, string channelId)
        => Channels.Add(userId, channelId);
    public static string GetChannelId(string userId)
    {
        string channelId = string.Empty;
        Channels.TryGetValue(userId, out channelId!);

        return channelId;
    }
    public static void RemoveChannel(string userId)
        => Channels.Remove(userId);

    public static void AddUser(string connId, string userId)
        => Users.Add(connId, userId);
    public static string GetUserId(string connId)
    {
        string userId = string.Empty;
        Users.TryGetValue(connId, out userId!);

        return userId;
    }
    public static void RemoveUser(string connId)
        => Users.Remove(connId);
}
