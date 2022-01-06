using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.TransactionActionModel
{
    public class TransactionActionModelFactory : ITransactionActionModelFactory
    {
        public DataAccess.Models.TransactionActionModel GetInstance()
        {
            var transactionAction = new DataAccess.Models.TransactionActionModel();
            transactionAction.Id = int.Parse(RandomGeneratorUtils.GenerateValidNumber(3));
            transactionAction.Name = RandomGeneratorUtils.GenerateString(20);
            return transactionAction;
        }
    }
}