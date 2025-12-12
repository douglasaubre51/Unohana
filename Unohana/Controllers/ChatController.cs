using Unohana.ViewModels.Chat;

namespace Unohana.Controllers;

public class ChatController(
    ChannelRepository channelRepo,
    TutorRepository tutorRepo,
    IHttpContextAccessor httpContextAccessor
) : Controller
{
    private readonly ChannelRepository _channelRepo = channelRepo;
    private readonly TutorRepository _tutorRepo = tutorRepo;
    private readonly IHttpContextAccessor _httpContext = httpContextAccessor;

    [Authorize(Roles = "TUTOR")]
    public ActionResult TutorChat()
    {
        try
        {
            List<Channel> dbChannels = _channelRepo.GetAll();
            Channel initialChannel = dbChannels.First();
            ChatViewModel viewModel = new()
            {
                Channels = dbChannels,
                CurrentChannel = initialChannel
            };

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
    public ActionResult SendMessage(ChatViewModel viewModel)
    {
        try
        {
            if (string.IsNullOrEmpty(viewModel.Message.Trim()))
                return RedirectToAction(
                    "TutorChat",
                    "Chat",
                    viewModel
                );

            string? tutorId = _httpContext.HttpContext!.Request.Cookies["Id"];
            Console.WriteLine("tutorid: " + tutorId);

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

            return RedirectToAction(
                "TutorChat",
                "Chat",
                viewModel
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SendMessage error: {ex.Message}");
            return RedirectToAction(
                "TutorChat",
                "Chat",
                viewModel
            );
        }
    }

}