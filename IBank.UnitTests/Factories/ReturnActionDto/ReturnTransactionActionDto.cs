using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ReturnTransactionActionDto
{
    public class ReturnTransactionActionDtoFactory : IReturnTransactionActionDtoFactory
    {
        public IBank.Dtos.TransactionAction.ReturnTransactionActionDto GetInstance()
        {
            var transactionAction = new IBank.Dtos.TransactionAction.ReturnTransactionActionDto();
            transactionAction.Name = RandomGeneratorUtils.GenerateString(20);
            return transactionAction;
        }
    }
}