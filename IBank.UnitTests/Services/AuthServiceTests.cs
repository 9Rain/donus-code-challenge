using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data.Client;
using DataAccess.Models;
using IBank.Dtos.Auth;
using IBank.Exceptions;
using IBank.Services.Auth;
using IBank.Services.Token;
using IBank.UnitTests.Factories.ClientModel;
using IBank.UnitTests.Factories.LoginAuthDto;
using IBank.UnitTests.Factories.TokenAuthDto;
using IBank.UnitTests.Utils;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace IBank.UnitTests.Services
{
    public class AuthServiceTests
    {
        private readonly ITokenAuthDtoFactory _tokenAuthDtoFactory;
        private readonly IClientModelFactory _clientModelFactory;
        private readonly ILoginAuthDtoFactory _loginAuthDtoFactory;
        private readonly Mock<IClientData> _clientDataStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly Mock<ITokenService> _tokenServiceStub;
        private readonly Mock<IConfiguration> _configStub;

        public AuthServiceTests(
            ITokenAuthDtoFactory tokenAuthDtoFactory,
            IClientModelFactory clientModelFactory,
            ILoginAuthDtoFactory loginAuthDtoFactory
        )
        {
            _tokenAuthDtoFactory = tokenAuthDtoFactory;
            _clientModelFactory = clientModelFactory;
            _loginAuthDtoFactory = loginAuthDtoFactory;

            _clientDataStub = new();
            _configStub = new();
            _mapperStub = new();
            _tokenServiceStub = new();
        }

        [Fact]
        public async Task Login_WithWrongCredentials_ThrowsAccountNotFoundException()
        {
            // Arrange
            _clientDataStub.Setup(data => data.Login(It.IsAny<AccountModel>()))
                .ReturnsAsync(It.IsAny<ClientModel>());

            _mapperStub.Setup(map => map.Map<AccountModel>(It.IsAny<LoginAuthDto>()))
                .Returns(It.IsAny<AccountModel>());

            var service = new AuthService(
                _clientDataStub.Object, _tokenServiceStub.Object, _mapperStub.Object, _configStub.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.Login(It.IsAny<LoginAuthDto>()));
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var token = _tokenAuthDtoFactory.GetInstance();
            var login = _loginAuthDtoFactory.GetInstance();
            var client = _clientModelFactory.GetInstance();

            client.Account.ShortPassword = Argon2.Hash(login.ShortPassword);

            _clientDataStub.Setup(data => data.Login(It.IsAny<AccountModel>()))
                .ReturnsAsync(client);

            _mapperStub.Setup(map => map.Map<AccountModel>(It.IsAny<LoginAuthDto>()))
                .Returns(It.IsAny<AccountModel>());

            _tokenServiceStub.Setup(service => service.GenerateToken(client.Id.ToString(), client.Name))
                .Returns(It.IsAny<string>());

            var configSectionStub = new Mock<IConfigurationSection>();
            configSectionStub.Setup(config => config.Value)
                .Returns(RandomGeneratorUtils.GenerateValidNumber(3));

            _configStub.Setup(config => config.GetSection(It.IsAny<string>()))
                .Returns(configSectionStub.Object);

            var service = new AuthService(
                _clientDataStub.Object, _tokenServiceStub.Object, _mapperStub.Object, _configStub.Object
            );

            // Act
            var result = await service.Login(login);

            // Assert
            Assert.IsType<TokenAuthDto>(result);
        }
    }
}
