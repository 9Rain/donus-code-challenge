using AutoMapper;
using IBank.Dtos.Auth;
using DataAccess.Models;
using IBank.Dtos.Account;
using IBank.Dtos.Transaction;
using IBank.Dtos.Agency;
using IBank.Dtos.Auth.Account.Agency;
using IBank.Dtos.TransactionAction;
using IBank.Dtos.Transaction.Addresse;
using IBank.Dtos.Transaction.Addresse.Account;
using IBank.Dtos.Transaction.Addresse.Account.Agency;

namespace IBank
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountModel, MeAuthDto>();
            CreateMap<AccountModel, ReturnAccountDto>();
            CreateMap<AgencyModel, ReturnAgencyDto>();
            CreateMap<ClientModel, MeAuthDto>();
            CreateMap<TransactionActionModel, ReturnTransactionActionDto>();
            CreateMap<TransactionModel, ReturnTransactionDto>();
            CreateMap<TransactionModel, ReturnListTransactionDto>();
            
            CreateMap<AccountTransactionDto, AccountModel>();
            CreateMap<AddresseTransactionDto, ClientModel>();
            CreateMap<AgencyAuthDto, AgencyModel>();
            CreateMap<AgencyTransactionDto, AgencyModel>();
            CreateMap<CreateAccountDto, ClientModel>();
            CreateMap<LoginAuthDto, AccountModel>();
        }
    }
}