using IBank.UnitTests.Factories.AccountModel;
using IBank.UnitTests.Utils;

namespace IBank.UnitTests.Factories.ClientModel
{
    public class ClientModelFactory : IClientModelFactory
    {
        private readonly IAccountModelFactory _account;

        public ClientModelFactory(IAccountModelFactory account)
        {
            _account = account;
        }

        public DataAccess.Models.ClientModel GetInstance()
        {
            var me = new DataAccess.Models.ClientModel();
            me.Id = long.Parse(RandomGeneratorUtils.GenerateValidNumber(1));
            me.Name = RandomGeneratorUtils.GenerateString(50);
            me.Cpf = RandomGeneratorUtils.GenerateCpf();
            me.Account = _account.GetInstance();
            return me;
        }
    }
}