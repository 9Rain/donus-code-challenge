using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ReturnAgencyDto
{
    public class ReturnAgencyDtoFactory : IReturnAgencyDtoFactory
    {
        public IBank.Dtos.Agency.ReturnAgencyDto GetInstance()
        {
            var agency = new IBank.Dtos.Agency.ReturnAgencyDto();
            agency.Number = RandomGeneratorUtils.GenerateValidNumber(4);
            agency.Digit = RandomGeneratorUtils.GenerateValidNumber(1);
            return agency;
        }
    }
}