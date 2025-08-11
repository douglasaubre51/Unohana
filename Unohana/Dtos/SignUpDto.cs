using System.ComponentModel.DataAnnotations;

namespace Unohana.Dtos
{
    public class SignUpDto
    {
        [Required]
        [MinLength(8, ErrorMessage = "password must be greater than 8 characters!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "passwords must match!")]
        [DataType(DataType.Password)]
        public string CheckPassword { get; set; } = string.Empty;
    }
}
