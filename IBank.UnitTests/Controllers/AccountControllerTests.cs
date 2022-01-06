using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using IBank.Controllers;
using IBank.Dtos.Account;
using IBank.Exceptions;
using IBank.Services.Account;
using IBank.Services.Token;
using IBank.UnitTests.Factories.ReturnAccountBalanceDto;
using IBank.UnitTests.Factories.ReturnAccountDto;
using IBank.UnitTests.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IBank.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly IReturnAccountDtoFactory _returnAccountDtoFactory;
        private readonly IReturnAccountBalanceDtoFactory _returnAccountBalanceDtoFactory;
        private readonly Mock<IAccountService> _accountServiceStub;
        private readonly Mock<ITokenService> _tokenServiceStub;

        public AccountControllerTests(
            IReturnAccountDtoFactory returnAccountDtoFactory,
            IReturnAccountBalanceDtoFactory returnAccountBalanceDtoFactory
        )
        {
            _returnAccountDtoFactory = returnAccountDtoFactory;
            _returnAccountBalanceDtoFactory = returnAccountBalanceDtoFactory;
            _accountServiceStub = new();
            _tokenServiceStub = new();

            _tokenServiceStub.Setup(service => service.GetIdFromToken(It.IsAny<ClaimsPrincipal>()))
                .Returns(RandomGeneratorUtils.GenerateValidNumber(1));
        }

        [Fact]
        public async Task My_WithUnexistingAccount_ReturnsInternalServerError()
        {
            // Arrange
            _accountServiceStub.Setup(service => service.GetByClientId(It.IsAny<long>()))
                .ThrowsAsync(new AccountNotFoundException());

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Get();
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task My_WithExistingAccount_ReturnsAccount()
        {
            var my = _returnAccountDtoFactory.GetInstance();

            // Arrange
            _accountServiceStub.Setup(service => service.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(my);

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Get();
            var okObjectResult = result.Result as OkObjectResult;

            // Assert
            okObjectResult.Value.Should().BeEquivalentTo(my);
        }

        [Fact]
        public async Task GetBalance_WithUnexistingAccount_ReturnsInternalServerError()
        {
            // Arrange
            _accountServiceStub.Setup(service => service.GetBalance(It.IsAny<long>()))
                .ThrowsAsync(new AccountNotFoundException());

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.GetBalance();
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task GetBalance_WithExistingAccount_ReturnsAccountBalance()
        {
            var balance = _returnAccountBalanceDtoFactory.GetInstance();

            // Arrange
            _accountServiceStub.Setup(service => service.GetBalance(It.IsAny<long>()))
                .ReturnsAsync(balance);

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.GetBalance();
            var okObjectResult = result.Result as OkObjectResult;

            // Assert
            okObjectResult.Value.Should().BeEquivalentTo(balance);
        }

        [Fact]
        public async Task Create_WithClientWithExistingAccount_ReturnsForbidden()
        {
            // Arrange
            _accountServiceStub.Setup(service => service.Create(It.IsAny<CreateAccountDto>()))
                .Throws(new ClientAlreadyHasAnAccountException());

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Create(It.IsAny<CreateAccountDto>());
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task Create_WithClientWithNoAccount_ReturnsAccount()
        {
            var account = _returnAccountDtoFactory.GetInstance();

            // Arrange
            _accountServiceStub.Setup(service => service.Create(It.IsAny<CreateAccountDto>()))
                .ReturnsAsync(account);

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var result = await controller.Create(It.IsAny<CreateAccountDto>());
            var createdObjectResult = result.Result as CreatedAtActionResult;

            // Assert
            createdObjectResult.Value.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task Delete_WithExistingOrUnexistingAccount_ReturnsNoContent()
        {
            // Arrange
            _accountServiceStub.Setup(service => service.DeleteByClientId(It.IsAny<long>()));

            var controller = new AccountController(_accountServiceStub.Object, _tokenServiceStub.Object);

            // Act
            var noContentObjectResult = await controller.Delete() as NoContentResult;

            // Assert
            Assert.NotNull(noContentObjectResult);
        }
    }
}
