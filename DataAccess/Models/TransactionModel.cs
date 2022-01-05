
using System;

namespace DataAccess.Models
{
    public class TransactionModel : BaseModel
    {
        public long Id { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DesiredCompletionDate { get; set; }
        public string ReferenceId { get; set; }
        public TransactionActionModel Action { get; set; }
        public AccountModel From { get; set; }
        public AccountModel To { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public bool IsCompleted { get; set; }
    }
}