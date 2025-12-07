using Unohana.ViewModels.Channel;

namespace Unohana.Controllers
{
    [Authorize]
    public class ChannelController(ChannelRepository channelRepo) : Controller
    {
        private readonly ChannelRepository _channelRepo = channelRepo;


        // All channels

        [Authorize(Roles = "TUTOR,STUDENT")]
        public ActionResult AllChannels()
            => View();


        // Channel Manager

        [Authorize(Roles = "TUTOR")]
        public ActionResult ChannelManager()
        {
            try
            {
                List<Channel> dbChannels = _channelRepo.GetAll();
                Mapper mapper = MapperUtility.GetMapper<Channel, ChannelDtos>();
                List<ChannelDtos> channelDtos = mapper.Map<List<ChannelDtos>>(
                    dbChannels.OrderByDescending(e => e.Id)
                );

                ChannelManagerViewModel viewModel = new()
                {
                    Channels = channelDtos
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChannelManager: get: error: " + ex.Message);
                return View();
            }
        }


        // Channel Manager actions :

        [Authorize(Roles = "TUTOR")]
        [HttpPost]
        public ActionResult AddNewChannel(ChannelManagerViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid is false)
                    return RedirectToAction(
                        "ChannelManager",
                        "Channel",
                        null
                    );

                // create and save channel to db !
                _channelRepo.Add(new Channel { Title = viewModel.ChannelTitleField });

                // load channels from db and map it !
                List<Channel> dbChannels = _channelRepo.GetAll();
                Mapper mapper = MapperUtility.GetMapper<Channel, ChannelDtos>();
                List<ChannelDtos> channelDtos = mapper.Map<List<ChannelDtos>>(dbChannels);
                viewModel.Channels = channelDtos;

                return RedirectToAction("ChannelManager", "Channel", viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddNewChannel error: " + ex.Message);
                return RedirectToAction(
                    "ChannelManager",
                    "Channel",
                    null
                );
            }
        }

        [Authorize(Roles = "TUTOR")]
        public async Task<ActionResult> EditChannel(int id)
        {
            try
            {
                Console.WriteLine("channel id: " + id);
                Channel? dbChannel = _channelRepo.GetById(id);
                if (dbChannel is null)
                {
                    Console.WriteLine("selected channel is null !");
                    return RedirectToAction(
                        "ChannelManager",
                        "Channel",
                        null
                    );
                }

                ChannelStudentDto channelStudentDto = new()
                {
                    Id = dbChannel.Id,
                    Title = dbChannel.Title,
                    Students = dbChannel.Students
                };


                return View(new EditChannelViewModel() { CurrentChannel = channelStudentDto });
            }
            catch (Exception ex)
            {
                Console.WriteLine("EditChannel error: " + ex.Message);
                return RedirectToAction(
                    "ChannelManager",
                    "Channel",
                    null
                );
            }
        }
        [HttpPost]
        [Authorize(Roles = "TUTOR")]
        public async Task<IActionResult> EditChannel(EditChannelViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid is false)
                    return View(viewModel);

                Channel? dbChannel = _channelRepo.GetById(viewModel.CurrentChannel!.Id);
                if (dbChannel is null)
                    return View(viewModel);

                dbChannel.Title = viewModel.CurrentChannel.Title;

                _channelRepo.Update(dbChannel);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EditChannel: post: error: " + ex.Message);
                return View(viewModel);
            }
        }

        // Delete action :

        [Authorize(Roles = "TUTOR")]
        public async Task<ActionResult> DeleteChannel(int id)
        {
            try
            {
                Console.WriteLine("channel id: " + id);
                return RedirectToAction(
                    "ChannelManager",
                    "Channel",
                    null
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("EditChannel error: " + ex.Message);
                return RedirectToAction(
                    "ChannelManager",
                    "Channel",
                    null
                );
            }
        }
    }
}
