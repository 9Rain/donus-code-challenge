using IBank.UnitTests.Factories.AgencyAuthDto;
using IBank.UnitTests.Factories.ReturnAccountDto;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.LoginAuthDto
{
    public class LoginAuthDtoFactory : ILoginAuthDtoFactory
    {
        private readonly IAgencyAuthDtoFactory _agencyAuthDtoFactory;

        public LoginAuthDtoFactory(IAgencyAuthDtoFactory agencyAuthDtoFactory)
        {
            _agencyAuthDtoFactory = agencyAuthDtoFactory;
        }

        public IBank.Dtos.Auth.LoginAuthDto GetInstance()
        {
            var login = new IBank.Dtos.Auth.LoginAuthDto();
            login.Agency = _agencyAuthDtoFactory.GetInstance();
            login.Number = RandomGeneratorUtils.GenerateValidNumber(5);
            login.Digit = RandomGeneratorUtils.GenerateValidNumber(1);
            login.ShortPassword = RandomGeneratorUtils.GenerateValidNumber(4);
            return login;
        }
    }
}