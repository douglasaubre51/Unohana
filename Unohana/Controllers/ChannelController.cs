namespace Unohana.Controllers
{
    public class ChannelController : Controller
    {
        [Authorize(Roles = "TUTOR")]
        public ActionResult ChannelManager()
            => View();

        [Authorize(Roles = "TUTOR,STUDENT")]
        public ActionResult AllChannels()
            => View();

    }
}
