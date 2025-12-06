namespace Unohana.ViewModels.Auth;

public class TutorSignInViewModel
{
    [EmailAddress(ErrorMessage = "Not an email !")]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public bool InvalidCredentials { get; set; }
}
