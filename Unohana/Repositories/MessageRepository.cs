namespace Unohana.Repositories;

public class MessageRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public Message? GetById(int messageId)
        => _context.Messages.SingleOrDefault(t => t.Id == messageId);
    public List<Message> GetAll()
        => [.. _context.Messages];

    public void Add(Message message)
    {
        _context.Messages.Add(message);
        Save();
    }

    void Save()
       => _context.SaveChanges();
}


