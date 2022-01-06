using IBank.UnitTests.Factories.ReturnAgencyDto;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ReturnAccountDto
{
    public class ReturnAccountDtoFactory : IReturnAccountDtoFactory
    {
        private readonly IReturnAgencyDtoFactory _agency;

        public ReturnAccountDtoFactory(IReturnAgencyDtoFactory agency)
        {
            _agency = agency;
        }

        public IBank.Dtos.Account.ReturnAccountDto GetInstance()
        {
            var account = new IBank.Dtos.Account.ReturnAccountDto();
            account.Agency = _agency.GetInstance();
            account.Number = RandomGeneratorUtils.GenerateValidNumber(4);
            account.Digit = RandomGeneratorUtils.GenerateValidNumber(1);
            return account;
        }
    }
}