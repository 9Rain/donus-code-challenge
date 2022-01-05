using AutoMapper;
using DataAccess.Data.Account;
using DataAccess.Data.Agency;
using DataAccess.Data.Client;
using DataAccess.Models;
using IBank.Dtos.Account;
using IBank.Dtos.Transaction.Addresse;
using IBank.Exceptions;
using IBank.Services.Client;
using Isopoh.Cryptography.Argon2;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IBank.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountData _accountData;
        private readonly IClientData _clientData;
        private readonly IAgencyData _agencyData;
        private readonly IMapper _mapper;

        private readonly IClientService _clientService;

        public AccountService(
            IAccountData accountData, 
            IClientData clientData, 
            IAgencyData agencyData,
            IMapper mapper,
            IClientService clientService
        )
        {
            _accountData = accountData;
            _clientData = clientData;
            _agencyData = agencyData;
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<ReturnAccountDto> GetByClientId(long clientId)
        {
            var account = await _accountData.GetByClientId(clientId);

            if (account == null)
            {
                throw new AccountNotFoundException();
            }

            return _mapper.Map<ReturnAccountDto>(account);
        }

        public async Task<ReturnAccountBalanceDto> GetBalance(long clientId)
        {
            var balance = await _accountData.GetBalance(clientId);

            if (balance == null)
            {
                throw new AccountNotFoundException();
            }

            return new ReturnAccountBalanceDto((decimal)balance);
        }

        public async Task<AccountModel> GetForTransaction(AddresseTransactionDto addresse)
        {
            addresse.Cpf = Regex.Replace(addresse.Cpf, "[^0-9]", "");

            var account = await _accountData.GetForTransaction(
                _mapper.Map<ClientModel>(addresse)
            );

            if (account == null)
            {
                throw new AccountNotFoundException();
            }

            return account;
        }

        public async Task<ReturnAccountDto> Create(CreateAccountDto clientData)
        {
            var client = await _clientService.Create(clientData);

            var agency = await _agencyData.Get(1);
            var accountNumber = await this.GenerateAccountNumber();
            var accountDigit = this.GenerateAccountDigit();
            var hashedShortPassword = Argon2.Hash(clientData.ShortPassword.Trim());
            var hashedPassword = Argon2.Hash(clientData.Password.Trim());

            var account = new AccountModel(
                agency, accountNumber, accountDigit, 
                hashedShortPassword, hashedPassword, client.Id
            );

            account.Id = await _accountData.Create(account);

            return _mapper.Map<ReturnAccountDto>(account);
        }

        public async Task DeleteByClientId(long clientId)
        {
            await _clientData.Delete(clientId);
        }

        private async Task<string> GenerateAccountNumber()
        {
            var rand = new Random();
            string number;

            do
            {
                number = rand.Next(10000, 100000).ToString();
            }
            while (await _accountData.GetByNumber(number) != null);

            return number;
        }

        private byte GenerateAccountDigit()
        {
            return (byte) new Random().Next(0, 10);
        }
    }
}