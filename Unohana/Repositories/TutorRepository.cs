namespace Unohana.Repositories;

public class TutorRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public IQueryable<Tutor> GetQueryable()
        => _context.Tutors;

    public Tutor? GetById(int tutorId)
        => _context.Tutors.SingleOrDefault(t => t.Id == tutorId);
    public List<Tutor> GetAll()
        => [.. _context.Tutors];

    public void Add(Tutor tutor)
    {
        _context.Tutors.Add(tutor);
        Save();
    }

    void Save()
       => _context.SaveChanges();
}
