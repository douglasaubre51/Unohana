namespace Unohana.Repositories;

public class ChannelRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public Channel? GetById(int channelId)
        => _context.Channels.Include(s=>s.Students)
                            .SingleOrDefault(t => t.Id == channelId);
    public List<Channel> GetAll()
        => [.. _context.Channels];

    public void Add(Channel channel)
    {
        _context.Channels.Add(channel);
        Save();
    }
    public void Update(Channel channel)
    {
        _context.Channels.Update(channel);
        Save();
    }
    public void Save()
       => _context.SaveChanges();
}

