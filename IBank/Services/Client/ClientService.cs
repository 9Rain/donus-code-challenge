using AutoMapper;
using DataAccess.Data.Client;
using DataAccess.Models;
using IBank.Dtos.Account;
using IBank.Dtos.Auth;
using IBank.Exceptions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IBank.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly IClientData _clientData;
        private readonly IMapper _mapper;

        public ClientService(IClientData clientData, IMapper mapper)
        {
            _clientData = clientData;
            _mapper = mapper;
        }

        public async Task<MeAuthDto> Get(long id)
        {
            var client = await _clientData.Get(id);

            if(client == null)
            {
                throw new ClientNotFoundException();
            }

            return _mapper.Map<MeAuthDto>(client);
        }

        public async Task<ClientModel> Create(CreateAccountDto clientData)
        {
            var cpf = Regex.Replace(clientData.Cpf.Trim(), "[^0-9]", "");

            var userHasAccount = await _clientData.HasActiveOrDisabledAccount(cpf);
            
            if (userHasAccount)
            {
                throw new ClientAlreadyHasAnAccountException(
                    "Client already has an active or disabled account"
                );
            }

            var client = new ClientModel(clientData.Name.Trim(), cpf);
            client.Id = await _clientData.Create(client);
            return client;
        }
    }

}
