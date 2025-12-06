namespace Unohana.Controllers;

public class AuthController(
    TutorRepository tutorRepo,
    StudentRepository studentRepo) : Controller
{
    private readonly TutorRepository _tutorRepo = tutorRepo;
    private readonly StudentRepository _studentRepo = studentRepo;

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

            Mapper mapper = MapperUtility.GetMapper<StudentSignUpViewModel, Student>();
            Student newStudent = mapper.Map<Student>(viewModel);
            _studentRepo.Add(newStudent);

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
            Console.WriteLine("StudentSignIn error: " + ex.Message);
            return View(viewModel);
        }
    }
}