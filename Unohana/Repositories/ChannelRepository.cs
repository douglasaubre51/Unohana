namespace Unohana.Repositories;

public class ChannelRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public Channel? GetById(int channelId)
        => _context.Channels.Include(s => s.Students)
                            .Include(m => m.Messages)!
                            .ThenInclude(t => t.Tutor)
                            .Include(t => t.Tutors)
                            .SingleOrDefault(t => t.Id == channelId);
    public List<Channel> GetAll()
        => [.. _context.Channels.Include(m => m.Messages)!.ThenInclude(t => t.Tutor).Include(t => t.Tutors)];

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

