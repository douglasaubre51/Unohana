using Microsoft.AspNetCore.Mvc;

namespace Unohana.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult TutorSignIn()
        {
            return View();
        }
        public ActionResult TutorSignUp()
        {
            return View();
        }

        public ActionResult StudentSignIn()
        {
            return View();
        }
        public ActionResult StudentSignUp()
        {
            return View();
        }
    }
}
