namespace Unohana.Repositories;

public class StudentRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public IQueryable<Student> GetQueryable()
        => _context.Students;

    public Student? GetById(int studentId)
        => _context.Students.SingleOrDefault(t => t.Id == studentId);
    public List<Student> GetAll()
        => [.. _context.Students.Include(c=>c.Channels)];

    public void Add(Student student)
    {
        _context.Students.Add(student);
        Save();
    }

    void Save()
       => _context.SaveChanges();
}

