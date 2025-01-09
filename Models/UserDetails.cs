using System.ComponentModel.DataAnnotations;
namespace Unohana.Models
{
    public class UserDetails
    {
        [Required(ErrorMessage = "First name is required!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters long!")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords doesnot match!")]
        public string? ConfirmPassword { get; set; }

        public string? Department { get; set; }
        public int Semester { get; set; }
        public int AdmissionNumber { get; set; }
        public int RollNo { get; set; }
    }
}