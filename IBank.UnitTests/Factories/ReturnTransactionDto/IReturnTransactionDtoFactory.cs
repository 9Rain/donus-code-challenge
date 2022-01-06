namespace IBank.UnitTests.Factories.ReturnTransactionDto
{
    public interface IReturnTransactionDtoFactory
    {
        IBank.Dtos.Transaction.ReturnTransactionDto GetInstance();
    }
}