using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ReturnAccountBalanceDto
{
    public class ReturnAccountBalanceDtoFactory : IReturnAccountBalanceDtoFactory
    {
        public IBank.Dtos.Account.ReturnAccountBalanceDto GetInstance()
        {
            return new IBank.Dtos.Account.ReturnAccountBalanceDto(
                decimal.Parse(RandomGeneratorUtils.GenerateValidNumber(3))
            );
        }
    }
}