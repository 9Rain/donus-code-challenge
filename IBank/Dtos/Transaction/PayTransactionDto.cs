using System;
using System.ComponentModel.DataAnnotations;

namespace IBank.Dtos.Transaction
{
    public class PayTransactionDto
    {
        [Required]
        [StringLength(48, MinimumLength = 47)]
        public string BarCode { get; set; }
    }
}
