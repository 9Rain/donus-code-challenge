using IBank.Dtos.Transaction.Addresse.Account;
using IBank.Validations;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction.Addresse
{
    public class AddresseTransactionDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be up to 100 characters in length.")]
        public string Name { get; set; }

        [Required]
        [Cpf]
        public string Cpf { get; set; }

        [Required]
        public AccountTransactionDto Account { get; set; }
    }
}
