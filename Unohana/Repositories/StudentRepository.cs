namespace Unohana.Repositories;

public class StudentRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public Student? GetById(int studentId)
        => _context.Students.SingleOrDefault(t => t.Id == studentId);
    public List<Student> GetAll()
        => [.. _context.Students];

    public void Add(Student student)
    {
        _context.Students.Add(student);
        Save();
    }

    void Save()
       => _context.SaveChanges();
}

