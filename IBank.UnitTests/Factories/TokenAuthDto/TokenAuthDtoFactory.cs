using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.TokenAuthDto
{
    public class TokenAuthDtoFactory : ITokenAuthDtoFactory
    {
        public IBank.Dtos.Auth.TokenAuthDto GetInstance()
        {
            return new IBank.Dtos.Auth.TokenAuthDto(
                int.Parse(RandomGeneratorUtils.GenerateValidNumber(4)),
                RandomGeneratorUtils.GenerateString(100)
            );
        }
    }
}