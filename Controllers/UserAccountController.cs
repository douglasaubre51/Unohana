using Microsoft.AspNetCore.Mvc;
using Unohana.ViewModels;

namespace Unohana.Controllers
{
    public class UserAccountController : Controller
    {
        public ActionResult CreateAccountView(CreateUserAccountVM model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Submit(CreateUserAccountVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {
                model.SqlErrorMessages = ex.Message;
                return View("SqlError", model);
            }

            return View("CreateAccountView", model);
        }

        public ActionResult SqlError(CreateUserAccountVM model)
        {
            return View(model);
        }
    }
}