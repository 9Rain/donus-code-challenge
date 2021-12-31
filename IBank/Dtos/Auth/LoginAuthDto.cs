using IBank.Dtos.Auth.Account;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Auth
{
    public class LoginAuthDto
    {
        [Required]
        public AccountAuthDto Account { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Short Password must contain numbers only.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Short Password must be 4 characters in length.")]
        public string ShortPassword { get; set; }

    }
}
