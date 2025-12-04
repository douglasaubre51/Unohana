namespace Unohana.Models;

public class Channel
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public List<Message>? Messages { get; set; }
    public List<Tutor>? Tutors { get; set; }
    public List<Student>? Students { get; set; }
}
