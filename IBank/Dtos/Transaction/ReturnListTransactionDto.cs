using IBank.Dtos.TransactionAction;
using System;
using System.Text.Json.Serialization;

namespace IBank.Dtos.Transaction
{
    public class ReturnListTransactionDto
    {
        public string ReferenceId { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("completionDate")]
        public DateTime? CompletedAt { get; set; } = null;

        public ReturnTransactionActionDto Action { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }
    }
}
