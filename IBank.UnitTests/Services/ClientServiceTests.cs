using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data.Client;
using DataAccess.Models;
using FluentAssertions;
using IBank.Dtos.Auth;
using IBank.Exceptions;
using IBank.Services.Client;
using IBank.UnitTests.Factories.ClientModel;
using IBank.UnitTests.Factories.CreateAccountDto;
using IBank.UnitTests.Factories.MeAuthDto;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace IBank.UnitTests.Services
{
    public class ClientServiceTests
    {
        private readonly IMeAuthDtoFactory _meAuthDtoFactory;
        private readonly IClientModelFactory _clientModelFactory;
        private readonly ICreateAccountDtoFactory _createAccountDtoFactory;
        private readonly Mock<IClientData> _clientData;
        private readonly Mock<IMapper> _mapper;

        public ClientServiceTests(
            IMeAuthDtoFactory meAuthDtoFactory,
            IClientModelFactory clientModelFactory,
            ICreateAccountDtoFactory createAccountDtoFactory
        )
        {
            _meAuthDtoFactory = meAuthDtoFactory;
            _clientModelFactory = clientModelFactory;
            _createAccountDtoFactory = createAccountDtoFactory;

            _clientData = new();
            _mapper = new();
        }

        [Fact]
        public async Task Get_WithUnexistingClientId_ThrowsClientNotFoundException()
        {
            // Arrange
            _clientData.Setup(data => data.Get(It.IsAny<long>()))
                .ReturnsAsync(It.IsAny<ClientModel>());

            var service = new ClientService(_clientData.Object, _mapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ClientNotFoundException>(() => service.Get(It.IsAny<long>()));
        }

        [Fact]
        public async Task Login_WithExistingClientId_ReturnsMeAuthDto()
        {
            // Arrange
            var me = _meAuthDtoFactory.GetInstance();
            var client = _clientModelFactory.GetInstance();

            _clientData.Setup(data => data.Get(It.IsAny<long>()))
                .ReturnsAsync(client);

            _mapper.Setup(map => map.Map<MeAuthDto>(It.IsAny<ClientModel>()))
                .Returns(me);

            var service = new ClientService(_clientData.Object, _mapper.Object);

            // Act
            var result = await service.Get(It.IsAny<long>());

            // Assert
            result.Should().BeEquivalentTo(me);
        }

        [Fact]
        public async Task Create_WithExistingAccountForProvidedCpf_ThrowsClientAlreadyHasAnAccountException()
        {
            // Arrange
            var account = _createAccountDtoFactory.GetInstance();

            _clientData.Setup(data => data.HasActiveOrDisabledAccount(It.IsAny<string>()))
                .ReturnsAsync(true);

            var service = new ClientService(_clientData.Object, _mapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ClientAlreadyHasAnAccountException>(() => service.Create(account));
        }

        [Fact]
        public async Task Create_WithUnexistingAccountForProvidedCpf_ReturnsClientModel()
        {
            // Arrange
            var account = _createAccountDtoFactory.GetInstance();

            _clientData.Setup(data => data.HasActiveOrDisabledAccount(It.IsAny<string>()))
                .ReturnsAsync(false);

            _clientData.Setup(data => data.Create(It.IsAny<ClientModel>()))
                .ReturnsAsync(It.IsAny<long>());

            var service = new ClientService(_clientData.Object, _mapper.Object);

            // Act
            var result = await service.Create(account);

            // Assert
            Assert.IsType<ClientModel>(result);
        }
    }
}
