namespace IBank.UnitTests.Factories.TransactionModel
{
    public interface ITransactionModelFactory
    {
        DataAccess.Models.TransactionModel GetInstance();
    }
}