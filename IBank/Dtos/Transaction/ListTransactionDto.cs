using System;

namespace IBank.Dtos.Transaction
{
    public class ListTransactionDto
    {
        public string ReferenceId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
