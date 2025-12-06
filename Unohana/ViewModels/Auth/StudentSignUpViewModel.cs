namespace Unohana.ViewModels.Auth;

public class StudentSignUpViewModel
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [StringLength(maximumLength: 12, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

}
