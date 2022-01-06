using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using IBank.Controllers;
using IBank.Dtos.Auth;
using IBank.Exceptions;
using IBank.Services.Auth;
using IBank.Services.Client;
using IBank.Services.Token;
using IBank.UnitTests.Factories.MeAuthDto;
using IBank.UnitTests.Factories.TokenAuthDto;
using IBank.UnitTests.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IBank.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly IMeAuthDtoFactory _meAuthDtoFactory;
        private readonly ITokenAuthDtoFactory _tokenAuthDtoFactory;
        private readonly Mock<IClientService> _clientServiceStub;
        private readonly Mock<ITokenService> _tokenServiceStub;
        private readonly Mock<IAuthService> _authServiceStub;

        public AuthControllerTests(
            IMeAuthDtoFactory meAuthDtoFactory,
            ITokenAuthDtoFactory tokenAuthDtoFactory
        )
        {
            _meAuthDtoFactory = meAuthDtoFactory;
            _tokenAuthDtoFactory = tokenAuthDtoFactory;
            _clientServiceStub = new();
            _tokenServiceStub = new();
            _authServiceStub = new();

            _tokenServiceStub.Setup(service => service.GetIdFromToken(It.IsAny<ClaimsPrincipal>()))
                .Returns(RandomGeneratorUtils.GenerateValidNumber(1));
        }

        [Fact]
        public async Task Me_WithUnexistingClient_ReturnsInternalServerError()
        {
            // Arrange
            _clientServiceStub.Setup(service => service.Get(It.IsAny<long>()))
                .ThrowsAsync(new ClientNotFoundException());

            var controller = new AuthController(_authServiceStub.Object, _clientServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Me();
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Me_WithExistingClient_ReturnsClient()
        {
            // Arrange
            var me = _meAuthDtoFactory.GetInstance();

            _clientServiceStub.Setup(service => service.Get(It.IsAny<long>()))
                .ReturnsAsync(me);

            var controller = new AuthController(_authServiceStub.Object, _clientServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Me();
            var okObjectResult = result.Result as OkObjectResult;

            // Assert
            okObjectResult.Value.Should().BeEquivalentTo(me);
        }

        [Fact]
        public async Task Login_WithWrongCredentials_ReturnsUnauthorized()
        {
            // Arrange
            _authServiceStub.Setup(service => service.Login(It.IsAny<LoginAuthDto>()))
                .ThrowsAsync(new AccountNotFoundException());

            var controller = new AuthController(_authServiceStub.Object, _clientServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Login(It.IsAny<LoginAuthDto>());
            var unauthorizedObjectResult = result.Result as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(unauthorizedObjectResult);
        }

        [Fact]
        public async Task Login_WithRightCredentials_ReturnsToken()
        {
            // Arrange
            var token = _tokenAuthDtoFactory.GetInstance();

            _authServiceStub.Setup(service => service.Login(It.IsAny<LoginAuthDto>()))
                .ReturnsAsync(token);

            var controller = new AuthController(_authServiceStub.Object, _clientServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Login(It.IsAny<LoginAuthDto>());
            var okObjectResult = result.Result as OkObjectResult;

            // Assert
            okObjectResult.Value.Should().BeEquivalentTo(token);
        }
    }
}
