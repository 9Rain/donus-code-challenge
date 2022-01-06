using IBank.UnitTests.Factories.AccountModel;
using IBank.UnitTests.Factories.AgencyAuthDto;
using IBank.UnitTests.Factories.AgencyModel;
using IBank.UnitTests.Factories.ClientModel;
using IBank.UnitTests.Factories.CreateAccountDto;
using IBank.UnitTests.Factories.LoginAuthDto;
using IBank.UnitTests.Factories.MeAuthDto;
using IBank.UnitTests.Factories.ReturnAccountBalanceDto;
using IBank.UnitTests.Factories.ReturnAccountDto;
using IBank.UnitTests.Factories.ReturnAgencyDto;
using IBank.UnitTests.Factories.ReturnListTransactionDto;
using IBank.UnitTests.Factories.ReturnTransactionActionDto;
using IBank.UnitTests.Factories.ReturnTransactionDto;
using IBank.UnitTests.Factories.TokenAuthDto;
using IBank.UnitTests.Factories.TransactionActionModel;
using IBank.UnitTests.Factories.TransactionModel;
using Microsoft.Extensions.DependencyInjection;

namespace IBank.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMeAuthDtoFactory, MeAuthDtoFactory>();
            services.AddTransient<ILoginAuthDtoFactory, LoginAuthDtoFactory>();
            services.AddTransient<IAgencyAuthDtoFactory, AgencyAuthDtoFactory>();
            services.AddTransient<ITokenAuthDtoFactory, TokenAuthDtoFactory>();
            services.AddTransient<ICreateAccountDtoFactory, CreateAccountDtoFactory>();
            services.AddTransient<IReturnAccountDtoFactory, ReturnAccountDtoFactory>();
            services.AddTransient<IReturnAccountBalanceDtoFactory, ReturnAccountBalanceDtoFactory>();
            services.AddTransient<IReturnAgencyDtoFactory, ReturnAgencyDtoFactory>();
            services.AddTransient<IReturnTransactionDtoFactory, ReturnTransactionDtoFactory>();
            services.AddTransient<IReturnTransactionActionDtoFactory, ReturnTransactionActionDtoFactory>();
            services.AddTransient<IReturnListTransactionDtoFactory, ReturnListTransactionDtoFactory>();
            services.AddTransient<IClientModelFactory, ClientModelFactory>();
            services.AddTransient<IAccountModelFactory, AccountModelFactory>();
            services.AddTransient<IAgencyModelFactory, AgencyModelFactory>();
            services.AddTransient<ITransactionModelFactory, TransactionModelFactory>();
            services.AddTransient<ITransactionActionModelFactory, TransactionActionModelFactory>();
        }
    }
}