using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ReturnTransactionDto
{
    public class ReturnTransactionDtoFactory : IReturnTransactionDtoFactory
    {
        public IBank.Dtos.Transaction.ReturnTransactionDto GetInstance()
        {
            var transaction = new IBank.Dtos.Transaction.ReturnTransactionDto();
            transaction.ReferenceId = RandomGeneratorUtils.GenerateString(100);
            return transaction;
        }
    }
}