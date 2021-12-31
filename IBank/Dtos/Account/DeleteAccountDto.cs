using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Account
{
    public class DeleteAccountDto
    {
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Short Password must be 4 characters in length.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Short Password must contain numbers only.")]
        public string ShortPassword { get; set; }
    }
}
