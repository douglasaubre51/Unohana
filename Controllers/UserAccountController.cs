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
                SetDBService setDBService = new SetDBService();

                string query1 = $@"insert into UserDetails (FirstName,LastName,Password) values (
                '{model.UserDetails.FirstName}',
                '{model.UserDetails.LastName}',
                '{model.UserDetails.Password}'
                )";

                try
                {
                    setDBService.InsertQuery(query1, _configuration);
                    return View("Success", model);
                }
                catch (Exception ex)
                {
                    model.SqlErrorMessages = ex.Message;
                    return View("SqlError", model);
                }
            }

            return View(model);
        }

        public ActionResult Success(CreateUserAccountVM model)
        {
            return View(model);
        }

        public ActionResult SqlError(CreateUserAccountVM model)
        {
            return View(model);
        }
    }
}