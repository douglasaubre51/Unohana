using Microsoft.AspNetCore.Mvc;
using Unohana.Services;
using Unohana.ViewModels;

namespace Unohana.Controllers
{
    public class UserAccountController : Controller
    {
        IConfiguration _configuration;
        public UserAccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult CreateAccountView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccountView(CreateUserAccountVM model)
        {
            if (ModelState.IsValid)
            {
                string query = "insert into "
                try
                {
                    SetDBService.InsertQuery();

                }
                catch (Exception ex)
                {
                    model.SqlErrorMessages = ex.Message;
                    return View("SqlError", model);
                }
            }

            return View(model);
        }


        public ActionResult SqlError(CreateUserAccountVM model)
        {
            return View(model);
        }
    }
}