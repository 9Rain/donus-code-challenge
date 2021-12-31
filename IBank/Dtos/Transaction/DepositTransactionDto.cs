using IBank.Dtos.Transaction.Addresse;
using IBank.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction
{
    public class DepositTransactionDto
    {
        [Required]
        public AddresseTransactionDto Addresse { get; set; }

        [Required]
        [Range(0.01, 2000, ErrorMessage = "Must be between 0.01 and 2000.")]
        public decimal Amount { get; set; }
    }
}
