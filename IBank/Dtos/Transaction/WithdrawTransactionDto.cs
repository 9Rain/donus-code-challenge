using System;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction
{
    public class WithdrawTransactionDto
    {
        [Required]
        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "Must be greater than 0.01.")]
        public decimal Amount { get; set; }
    }
}
