using Microsoft.AspNetCore.Mvc;

namespace Unohana.Controllers
{
    public class UserAccountController : Controller
    {
        public ActionResult CreateAccountView()
        {
            return View();
        }
    }
}