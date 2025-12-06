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
        public ActionResult TutorSignUp()
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
        [HttpPost]
        public ActionResult TutorSignUp(TutorSignUpViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid is false)
                    return View(viewModel);

                Console.WriteLine(viewModel.Email);
                Console.WriteLine(viewModel.Password);
                Console.WriteLine(viewModel.EmployeeId);
                Console.WriteLine(viewModel.FirstName);
                Console.WriteLine(viewModel.LastName);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TutorSignIn error: " + ex.Message);
                return View(viewModel);
            }
        }


        public ActionResult StudentSignIn()
        {
            return View();
        }
        public ActionResult StudentSignUp()
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
        [HttpPost]
        public ActionResult StudentSignUp(StudentSignUpViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid is false)
                    return View(viewModel);

                Console.WriteLine(viewModel.Email);
                Console.WriteLine(viewModel.Password);
                Console.WriteLine(viewModel.StudentId);
                Console.WriteLine(viewModel.FirstName);
                Console.WriteLine(viewModel.LastName);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TutorSignIn error: " + ex.Message);
                return View(viewModel);
            }
        }

    }
}
