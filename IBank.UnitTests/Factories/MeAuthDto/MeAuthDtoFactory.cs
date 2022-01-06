using IBank.UnitTests.Factories.ReturnAccountDto;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.MeAuthDto
{
    public class MeAuthDtoFactory : IMeAuthDtoFactory
    {
        private readonly IReturnAccountDtoFactory _account;

        public MeAuthDtoFactory(IReturnAccountDtoFactory account)
        {
            _account = account;
        }

        public IBank.Dtos.Auth.MeAuthDto GetInstance()
        {
            var me = new IBank.Dtos.Auth.MeAuthDto();
            me.Name = RandomGeneratorUtils.GenerateString(50);
            me.Cpf = RandomGeneratorUtils.GenerateCpf();
            me.Account = _account.GetInstance();
            return me;
        }
    }
}