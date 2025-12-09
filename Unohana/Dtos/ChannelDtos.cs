namespace Unohana.Dtos;

public class ChannelDtos
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class ChannelStudentDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public List<Student> Students { get; set; } = [];
}
