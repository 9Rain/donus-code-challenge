using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction.Addresse.Account.Agency
{
    public class AgencyTransactionDto
    {
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Number must contain numbers only.")]
        public string Number { get; set; }
        
        [Required]
        [RegularExpression(@"^[0-9]$", ErrorMessage = "Digit must be a single number.")]
        public string Digit { get; set; }
    }
}
