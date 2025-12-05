namespace Unohana.ViewModels.Auth;

public class TutorSignInViewModel
{
    [EmailAddress(ErrorMessage = "Not an email !")]
    public string Email { get; set; } = string.Empty;

    [StringLength(maximumLength: 12, MinimumLength = 8, ErrorMessage = "Password too short !")]
    public string Password { get; set; } = string.Empty;
}
