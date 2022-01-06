using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.CreateAccountDto
{
    public class CreateAccountDtoFactory : ICreateAccountDtoFactory
    {
        public IBank.Dtos.Account.CreateAccountDto GetInstance()
        {
            var account = new IBank.Dtos.Account.CreateAccountDto();
            account.Name = RandomGeneratorUtils.GenerateString(50);
            account.Cpf = RandomGeneratorUtils.GenerateCpf();
            account.ShortPassword = RandomGeneratorUtils.GenerateString(4);
            account.Password = RandomGeneratorUtils.GenerateString(6);
            return account;
        }
    }
}