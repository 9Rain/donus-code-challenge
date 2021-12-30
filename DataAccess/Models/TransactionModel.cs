
using System;

namespace DataAccess.Models
{
    public class TransactionModel : BaseModel
    {
        public DateTime CompletedAt { get; set; }
        public DateTime DesiredCompletionDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsIncome { get; set; }

        public int ActionId { get; set; }
        public long FromAccount { get; set; }
        public long ToAccount { get; set; }
    }
}