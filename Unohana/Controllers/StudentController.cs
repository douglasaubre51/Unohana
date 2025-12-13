namespace Unohana.Controllers;

public class StudentController(
    ChannelRepository channelRepo,
    StudentRepository studentRepo
) : Controller
{
    private readonly ChannelRepository _channelRepo = channelRepo;
    private readonly StudentRepository _studentRepo = studentRepo;

    [Authorize(Roles = "STUDENT")]
    public ActionResult LoadChatRoom(int id)
    {
        try
        {
            TempData["SelectedChannelId"] = id;
            return RedirectToAction(
                "StudentChat",
                "Student",
                null
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Student: LoadChatRoom error: {ex.Message}");
            return RedirectToAction(
                "StudentChat",
                "Student",
                null
            );
        }
    }

    [Authorize(Roles = "STUDENT")]
    public ActionResult StudentChat()
    {
        try
        {
            Student? dbStudent = _studentRepo.GetById(int.Parse(Request.Cookies["Id"]!));
            List<Channel> dbChannels = _channelRepo.GetQueryable()
                                                    .Include(s => s.Students)
                                                    .Include(m => m.Messages)!
                                                    .ThenInclude(t => t.Tutor)
                                                    .Where(c => c.Students!.Contains(dbStudent!))
                                                    .ToList();

            int id = TempData["SelectedChannelId"] as int? ?? 0;
            Console.WriteLine($"current ChannelId: {id}");

            Channel initialChannel = (id != 0) ? _channelRepo.GetById(id)! : dbChannels.First();
            ChatViewModel viewModel = new()
            {
                Channels = dbChannels,
                CurrentChannel = initialChannel
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine("StudentChat error: " + ex.Message);
            return View();
        }
    }
}