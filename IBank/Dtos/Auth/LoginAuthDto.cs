using IBank.Dtos.Auth.Account.Agency;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Auth
{
    public class LoginAuthDto
    {
        [Required]
        public AgencyAuthDto Agency { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Number must contain numbers only.")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]$", ErrorMessage = "Digit must be a single number.")]
        public string Digit { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Short Password must contain numbers only.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Short Password must be 4 characters in length.")]
        public string ShortPassword { get; set; }
    }
}
