using IBank.UnitTests.Factories.AccountModel;
using IBank.UnitTests.Factories.TransactionActionModel;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.TransactionModel
{
    public class TransactionModelFactory : ITransactionModelFactory
    {
        private readonly IAccountModelFactory _account;
        private readonly ITransactionActionModelFactory _transactionAction;

        public TransactionModelFactory(
            IAccountModelFactory account,
            ITransactionActionModelFactory transactionAction
        )
        {
            _account = account;
            _transactionAction = transactionAction;
        }

        public DataAccess.Models.TransactionModel GetInstance()
        {
            var transaction = new DataAccess.Models.TransactionModel();
            transaction.Id = long.Parse(RandomGeneratorUtils.GenerateValidNumber(4));
            transaction.IsActive = true;
            transaction.IsCompleted = true;
            transaction.IsIncome = false;
            transaction.ReferenceId = RandomGeneratorUtils.GenerateString(50);
            transaction.From = _account.GetInstance();
            transaction.To = _account.GetInstance();
            return transaction;
        }
    }
}