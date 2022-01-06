using System;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data.Account;
using DataAccess.Data.Agency;
using DataAccess.Data.Client;
using DataAccess.Models;
using FluentAssertions;
using IBank.Dtos.Account;
using IBank.Dtos.Transaction.Addresse;
using IBank.Exceptions;
using IBank.Services.Account;
using IBank.Services.Client;
using IBank.UnitTests.Factories.CreateAccountDto;
using IBank.UnitTests.Factories.ReturnAccountBalanceDto;
using IBank.UnitTests.Factories.ReturnAccountDto;
using IBank.UnitTests.Utils;
using Moq;
using Xunit;

namespace IBank.UnitTests.Services
{
    public class AccountServiceTests
    {
        private readonly IReturnAccountDtoFactory _returnAccountDtoFactory;
        private readonly ICreateAccountDtoFactory _createAccountDtoFactory;
        private readonly IReturnAccountBalanceDtoFactory _returnAccountBalanceDtoFactory;
        private readonly Mock<IAccountData> _accountData;
        private readonly Mock<IClientData> _clientData;
        private readonly Mock<IAgencyData> _agencyData;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IClientService> _clientService;

        public AccountServiceTests(
            IReturnAccountDtoFactory returnAccountDtoFactory,
            ICreateAccountDtoFactory createAccountDtoFactory,
            IReturnAccountBalanceDtoFactory returnAccountBalanceDtoFactory
        )
        {
            _returnAccountDtoFactory = returnAccountDtoFactory;
            _createAccountDtoFactory = createAccountDtoFactory;
            _returnAccountBalanceDtoFactory = returnAccountBalanceDtoFactory;

            _accountData = new();
            _clientData = new();
            _agencyData = new();
            _mapper = new();
            _clientService = new();
        }

        [Fact]
        public async Task GetByClientId_WithUnexistingClientId_ThrowsAccountNotFoundException()
        {
            // Arrange
            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync((AccountModel)null);

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.GetByClientId(It.IsAny<long>()));
        }

        [Fact]
        public async Task GetByClientId_WithExistingClientId_ReturnsReturnAccountDto()
        {
            // Arrange
            var account = _returnAccountDtoFactory.GetInstance();

            _accountData.Setup(data => data.GetByClientId(It.IsNotNull<long>()))
                .ReturnsAsync(new AccountModel());

            _mapper.Setup(map => map.Map<ReturnAccountDto>(It.IsAny<AccountModel>()))
                .Returns(account);

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act
            var result = await service.GetByClientId(It.IsNotNull<long>());

            // Assert
            result.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task GetBalance_WithUnexistingClientId_ThrowsAccountNotFoundException()
        {
            // Arrange
            _accountData.Setup(data => data.GetBalance(It.IsAny<long>()))
                .ReturnsAsync((Decimal?)null);

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.GetBalance(It.IsAny<long>()));
        }

        [Fact]
        public async Task GetBalance_WithExistingClientId_ReturnsReturnAccountBalanceDto()
        {
            // Arrange
            var accountBalance = _returnAccountBalanceDtoFactory.GetInstance();

            _accountData.Setup(data => data.GetBalance(It.IsNotNull<long>()))
                .ReturnsAsync(accountBalance.Balance);

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act
            var result = await service.GetBalance(It.IsNotNull<long>());

            // Assert
            Assert.Equal(result.Balance, accountBalance.Balance);
        }

        [Fact]
        public async Task GetForTransaction_WithUnexistingAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            var addresse = new AddresseTransactionDto();
            addresse.Cpf = RandomGeneratorUtils.GenerateCpf();

            _accountData.Setup(data => data.GetForTransaction(It.IsAny<ClientModel>()))
                .ReturnsAsync((AccountModel)null);

            _mapper.Setup(map => map.Map<ClientModel>(It.IsAny<AddresseTransactionDto>()))
                .Returns(It.IsAny<ClientModel>());

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.GetForTransaction(addresse));
        }

        [Fact]
        public async Task GetForTransaction_WithExistingAccount_ReturnsAccount()
        {
            // Arrange
            var addresse = new AddresseTransactionDto();
            addresse.Cpf = RandomGeneratorUtils.GenerateCpf();

            var account = new AccountModel();

            _accountData.Setup(data => data.GetForTransaction(It.IsAny<ClientModel>()))
                .ReturnsAsync(account);

            _mapper.Setup(map => map.Map<ClientModel>(It.IsAny<AddresseTransactionDto>()))
                .Returns(It.IsAny<ClientModel>());

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act
            var result = await service.GetForTransaction(addresse);

            // Assert
            result.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task Create_WithExistingAccountForProvidedCpf_ThrowsClientAlreadyHasAnAccountException()
        {
            // Arrange
            _clientService.Setup(service => service.Create(It.IsAny<CreateAccountDto>()))
                .ThrowsAsync(new ClientAlreadyHasAnAccountException());


            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<ClientAlreadyHasAnAccountException>(() => service.Create(It.IsAny<CreateAccountDto>()));
        }

        [Fact]
        public async Task Create_WithUnexistingAccountForProvidedCpf_ReturnsReturnAccountDto()
        {
            // Arrange
            var create = _createAccountDtoFactory.GetInstance();
            var account = _returnAccountDtoFactory.GetInstance();

            _clientService.Setup(service => service.Create(It.IsAny<CreateAccountDto>()))
                .ReturnsAsync(new ClientModel());

            _agencyData.Setup(agency => agency.Get(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<AgencyModel>());

            _accountData.Setup(data => data.GetByNumber(It.IsAny<string>()))
                .ReturnsAsync((AccountModel)null);

            _accountData.Setup(data => data.Create(It.IsNotNull<AccountModel>()))
                .ReturnsAsync(It.IsAny<long>());

            _mapper.Setup(map => map.Map<ReturnAccountDto>(It.IsAny<AccountModel>()))
                .Returns(account);

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act
            var result = await service.Create(create);

            // Assert
            result.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task DeleteByClientId_WithExistingOrUnexistingAccount_ReturnsVoid()
        {
            // Arrange
            _clientData.Setup(data => data.Delete(It.IsAny<long>()));

            var service = new AccountService(
                _accountData.Object, _clientData.Object, _agencyData.Object,
                _mapper.Object, _clientService.Object
            );

            // Act
            await service.DeleteByClientId(It.IsAny<long>());

            // Assert
            Assert.True(true);
        }
    }
}
