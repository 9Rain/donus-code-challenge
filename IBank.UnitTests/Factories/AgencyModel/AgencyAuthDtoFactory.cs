using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.AgencyModel
{
    public class AgencyModelFactory : IAgencyModelFactory
    {
        public DataAccess.Models.AgencyModel GetInstance()
        {
            var agency = new DataAccess.Models.AgencyModel();
            agency.Number = RandomGeneratorUtils.GenerateValidNumber(4);
            agency.Digit = byte.Parse(RandomGeneratorUtils.GenerateValidNumber(1));
            return agency;
        }
    }
}