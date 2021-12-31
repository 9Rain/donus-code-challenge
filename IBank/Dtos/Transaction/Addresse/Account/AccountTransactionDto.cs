using IBank.Dtos.Transaction.Addresse.Account.Agency;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction.Addresse.Account
{
    public class AccountTransactionDto
    {
        [Required]
        public AgencyTransactionDto Agency { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Number must contain numbers only.")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]$", ErrorMessage = "Digit must be a single number.")]
        public string Digit { get; set; }
    }
}
