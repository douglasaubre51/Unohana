using System.ComponentModel.DataAnnotations;

namespace Unohana.ValidationDtos
{
    public class SignInDto
    {
        [Required]
        public double IdentificationNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
