namespace Unohana.Controllers;

public class AuthController(
    TutorRepository tutorRepo,
    StudentRepository studentRepo) : Controller
{
    private readonly TutorRepository _tutorRepo = tutorRepo;
    private readonly StudentRepository _studentRepo = studentRepo;


    // TUTOR Actions :
    public ActionResult TutorSignIn()
        => View();
    public ActionResult TutorSignUp()
        => View();

    [HttpPost]
    public async Task<ActionResult> TutorSignIn(TutorSignInViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid is false)
                return View(viewModel);

            if (AccountValidator.ValidateTutor(
                viewModel.Email,
                viewModel.Password,
                _tutorRepo.GetQueryable()) is false)
            {
                viewModel.InvalidCredentials = true;
                return View(viewModel);
            }

            Tutor dbTutor = _tutorRepo.GetQueryable()
                .Where(e => e.Email == viewModel.Email)
                .Single();

            // SignIn logic
            CookieDtos dto = CookieAuthUtility.InitTutorCookie(dbTutor);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(dto.ClaimsIdentity),
                dto.AuthenticationProperties
            );

            HttpContext.Response.Cookies.Append("Id", dbTutor.Id.ToString());
            HttpContext.Response.Cookies.Append("Role", Roles.TUTOR.ToString());

            return RedirectToAction("ChannelManager", "Channel");
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

            Mapper mapper = MapperUtility.GetMapper<TutorSignUpViewModel, Tutor>();
            Tutor newTutor = mapper.Map<Tutor>(viewModel);
            _tutorRepo.Add(newTutor);

            return View(viewModel);
        }
        catch (UniqueConstraintException ex)
        {
            Console.WriteLine("Duplicate account error: " + ex.Message);
            viewModel.DuplicateAccountError = true;
            return View(viewModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine("TutorSignIn error: " + ex.Message);
            return View(viewModel);
        }
    }


    // STUDENT Actions :

    public ActionResult StudentSignIn()
        => View();
    public ActionResult StudentSignUp()
        => View();

    [HttpPost]
    public async Task<ActionResult> StudentSignIn(StudentSignInViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid is false)
                return View(viewModel);

            if (AccountValidator.ValidateStudent(
                viewModel.Email,
                viewModel.Password,
                _studentRepo.GetQueryable()) is false)
            {
                viewModel.InvalidCredentials = true;
                return View(viewModel);
            }

            Student dbStudent = _studentRepo.GetQueryable()
                .Where(e => e.Email == viewModel.Email)
                .Single();

            // signin logic
            CookieDtos dto = CookieAuthUtility.InitStudentCookie(dbStudent);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(dto.ClaimsIdentity),
                dto.AuthenticationProperties
            );
            HttpContext.Response.Cookies.Append("Id", dbStudent.Id.ToString());
            HttpContext.Response.Cookies.Append("Role", Roles.STUDENT.ToString());

            return RedirectToAction("StudentChat", "Student");
        }
        catch (Exception ex)
        {
            Console.WriteLine("StudentSignIn error: " + ex.Message);
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

            Mapper mapper = MapperUtility.GetMapper<StudentSignUpViewModel, Student>();
            Student newStudent = mapper.Map<Student>(viewModel);
            _studentRepo.Add(newStudent);

            return RedirectToAction("StudentSignIn", "Auth");
        }
        catch (UniqueConstraintException ex)
        {
            Console.WriteLine("Duplicate account error: " + ex.Message);
            viewModel.DuplicateAccountError = true;
            return View(viewModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine("StudentSignIn error: " + ex.Message);
            return View(viewModel);
        }
    }


    // Common Actions :

    public async Task<ActionResult> SignOutUser()
    {
        try
        {
            Console.WriteLine("Signing out tutor ...");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine("TutorSignOut error: " + ex.Message);
            return View();
        }
    }
}