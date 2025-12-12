namespace Unohana.ViewModels.Channel;

public class EditChannelViewModel
{
    public ChannelStudentDto? CurrentChannel { get; set; }
    public List<Student> AvailableStudents { get; set; } = [];
    public List<Student> JoinedStudents { get; set; } = [];
}
