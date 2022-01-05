using IBank.Dtos.Transaction.Addresse;
using System;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction
{
    public class TransferTransactionDto
    {
        [Required]
        public AddresseTransactionDto Addresse { get; set; }

        [Required]
        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "Must be greater than 0.01.")]
        public decimal Amount { get; set; }
    }
}
