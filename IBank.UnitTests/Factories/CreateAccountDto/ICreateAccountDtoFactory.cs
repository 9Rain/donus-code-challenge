namespace IBank.UnitTests.Factories.CreateAccountDto
{
    public interface ICreateAccountDtoFactory
    {
        IBank.Dtos.Account.CreateAccountDto GetInstance();
    }
}