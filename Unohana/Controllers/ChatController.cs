namespace Unohana.Controllers;

public class ChatController(
    ChannelRepository channelRepo,
    TutorRepository tutorRepo
) : Controller
{
    private readonly ChannelRepository _channelRepo = channelRepo;
    private readonly TutorRepository _tutorRepo = tutorRepo;

    [Authorize(Roles = "TUTOR")]
    public ActionResult LoadChatRoom(int id)
    {
        try
        {
            TempData["SelectedChannelId"] = id;
            return RedirectToAction(
                "TutorChat",
                "Chat",
                null
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LoadChatRoom error: {ex.Message}");
            return RedirectToAction(
                "TutorChat",
                "Chat",
                null
            );
        }
    }

    [Authorize(Roles = "TUTOR,STUDENT")]
    public ActionResult TutorChat()
    {
        try
        {
            // send students to student channel controller !
            if (Request.Cookies["Role"] == Roles.STUDENT.ToString())
            {
                return RedirectToAction(
                    "StudentChat",
                    "Student",
                    null
                );
            }

            List<Channel> dbChannels = _channelRepo.GetAll();

            int id = TempData["SelectedChannelId"] as int? ?? 0;
            Console.WriteLine($"current ChannelId: {id}");

            string? tutorId = HttpContext.Request.Cookies["Id"];
            if (string.IsNullOrWhiteSpace(tutorId))
            {
                tutorId = HttpContext.User.Claims.FirstOrDefault(key => key.Type == "Id")!.Value;
                Console.WriteLine("Tutor id from Auth Cookie: " + tutorId);
            }

            Channel initialChannel = (id != 0) ? _channelRepo.GetById(id)! : dbChannels.First();
            ChatViewModel viewModel = new()
            {
                Channels = dbChannels,
                CurrentChannel = initialChannel,
                UserId = tutorId
            };

            Console.WriteLine("channel id: " + viewModel.Channels.FirstOrDefault()!.Id);

            return View(viewModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine("TutorChat error: " + ex.Message);
            return View();
        }
    }

    [Authorize(Roles = "TUTOR")]
    [HttpPost]
    public async Task<ActionResult> SendMessage(ChatViewModel viewModel)
    {
        try
        {
            if (string.IsNullOrEmpty(viewModel.Message.Trim()))
                return RedirectToAction(
                    "TutorChat",
                    "Chat",
                    viewModel
                );

            string? tutorId = HttpContext.Request.Cookies["Id"];
            if (string.IsNullOrWhiteSpace(tutorId))
            {
                tutorId = HttpContext.User.Claims.FirstOrDefault(key => key.Type == "Id")!.Value;
                Console.WriteLine("Tutor id from Auth Cookie: " + tutorId);
            }

            Console.WriteLine("Tutor id: " + tutorId);

            if (string.IsNullOrEmpty(tutorId))
                return RedirectToAction(
                    "TutorChat",
                    "Chat",
                    viewModel
                );

            var dbTutor = _tutorRepo.GetById(int.Parse(tutorId));
            var dbChannel = _channelRepo.GetById(viewModel.CurrentChannel!.Id);
            dbChannel!.Messages!.Add(new Message()
            {
                Text = viewModel.Message,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
                Tutor = dbTutor!,
                Channel = dbChannel
            });
            _channelRepo.Save();

            TempData["SelectedChannelId"] = viewModel.CurrentChannel.Id;
            return RedirectToAction(
                "TutorChat",
                "Chat"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SendMessage error: {ex.Message}");
            return RedirectToAction(
                "TutorChat",
                "Chat"
            );
        }
    }
}