using System;
using IBank.UnitTests.Factories.ReturnTransactionActionDto;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ReturnListTransactionDto
{
    public class ReturnListTransactionDtoFactory : IReturnListTransactionDtoFactory
    {
        private readonly IReturnTransactionActionDtoFactory _returnTransactionActionDtoFactory;
        public ReturnListTransactionDtoFactory(
            IReturnTransactionActionDtoFactory returnTransactionActionDtoFactory
        )
        {
            _returnTransactionActionDtoFactory = returnTransactionActionDtoFactory;
        }

        public IBank.Dtos.Transaction.ReturnListTransactionDto GetInstance()
        {
            var transaction = new IBank.Dtos.Transaction.ReturnListTransactionDto();
            transaction.CreatedAt = DateTime.Now;
            transaction.CompletedAt = DateTime.Now;
            transaction.Type = RandomGeneratorUtils.GenerateString(10);
            transaction.ReferenceId = RandomGeneratorUtils.GenerateString(100);
            transaction.Amount = decimal.Parse(RandomGeneratorUtils.GenerateValidNumber(3));
            transaction.Action = _returnTransactionActionDtoFactory.GetInstance();
            return transaction;
        }
    }
}