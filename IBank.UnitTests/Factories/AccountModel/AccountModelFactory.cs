using IBank.UnitTests.Factories.AgencyModel;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.AccountModel
{
    public class AccountModelFactory : IAccountModelFactory
    {
        private readonly IAgencyModelFactory _agency;

        public AccountModelFactory(IAgencyModelFactory agency)
        {
            _agency = agency;
        }

        public DataAccess.Models.AccountModel GetInstance()
        {
            var account = new DataAccess.Models.AccountModel();
            account.Agency = _agency.GetInstance();
            account.Number = RandomGeneratorUtils.GenerateValidNumber(4);
            account.Digit = byte.Parse(RandomGeneratorUtils.GenerateValidNumber(1));
            account.ShortPassword = RandomGeneratorUtils.GenerateValidNumber(4);
            account.Password = RandomGeneratorUtils.GenerateValidNumber(6);
            return account;
        }
    }
}