namespace Unohana.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
 : DbContext(options)
{
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tutor>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(e => e.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
