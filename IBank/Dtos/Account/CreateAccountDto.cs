using System.ComponentModel.DataAnnotations;
using IBank.Validations;

namespace IBank.Dtos.Account
{
    public class CreateAccountDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Cpf]
        public string Cpf { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Short Password must be 4 characters in length.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Short Password must contain numbers only.")]
        public string ShortPassword { get; set; }
        
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be 6 characters in length.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Password must contain numbers only.")]
        public string Password { get; set; }
    }
}