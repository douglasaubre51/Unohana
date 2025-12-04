namespace Unohana.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
 : DbContext(options)
{
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Student> Students { get; set; }
}
