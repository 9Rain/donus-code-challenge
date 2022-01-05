using FoolProof.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction
{
    public class ListTransactionDto
    {
        [Required]
        [LessThan("EndDate", ErrorMessage = "Is required and must be less than EndDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [GreaterThan("StartDate", ErrorMessage = "Is required and must be greater than StartDate")]
        public DateTime EndDate { get; set; }
    }
}
