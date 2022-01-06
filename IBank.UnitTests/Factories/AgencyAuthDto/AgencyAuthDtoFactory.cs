using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.AgencyAuthDto
{
    public class AgencyAuthDtoFactory : IAgencyAuthDtoFactory
    {
        public IBank.Dtos.Auth.Account.Agency.AgencyAuthDto GetInstance()
        {
            var agency = new IBank.Dtos.Auth.Account.Agency.AgencyAuthDto();
            agency.Number = RandomGeneratorUtils.GenerateValidNumber(4);
            agency.Digit = RandomGeneratorUtils.GenerateValidNumber(1);
            return agency;
        }
    }
}