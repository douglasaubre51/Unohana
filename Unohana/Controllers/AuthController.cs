using Microsoft.AspNetCore.Mvc;
using Unohana.ViewModels.Auth;

namespace Unohana.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult TutorSignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TutorSignIn(TutorSignInViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid is false)
                    return View(viewModel);

                Console.WriteLine(viewModel.Email);
                Console.WriteLine(viewModel.Password);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TutorSignIn error: " + ex.Message);
                return View(viewModel);
            }
        }
        public ActionResult TutorSignUp()
        {
            return View();
        }


        public ActionResult StudentSignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentSignIn(StudentSignInViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid is false)
                    return View(viewModel);

                Console.WriteLine(viewModel.Email);
                Console.WriteLine(viewModel.Password);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TutorSignIn error: " + ex.Message);
                return View(viewModel);
            }
        }
        public ActionResult StudentSignUp()
        {
            return View();
        }
    }
}
