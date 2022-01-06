using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using IBank.Controllers;
using IBank.Dtos.Transaction;
using IBank.Exceptions;
using IBank.Services.Token;
using IBank.Services.Transaction;
using IBank.UnitTests.Factories.ReturnListTransactionDto;
using IBank.UnitTests.Factories.ReturnTransactionDto;
using IBank.UnitTests.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IBank.UnitTests.Controllers
{
    public class TransactionControllerTests
    {
        private readonly IReturnListTransactionDtoFactory _returnListTransactionDtoFactory;
        private readonly IReturnTransactionDtoFactory _returnTransactionDtoFactory;
        private readonly Mock<ITransactionService> _transactionServiceStub;
        private readonly Mock<ITokenService> _tokenServiceStub;

        public TransactionControllerTests(
            IReturnListTransactionDtoFactory returnListTransactionDtoFactory,
            IReturnTransactionDtoFactory returnTransactionDtoFactory
        )
        {
            _returnListTransactionDtoFactory = returnListTransactionDtoFactory;
            _returnTransactionDtoFactory = returnTransactionDtoFactory;
            _transactionServiceStub = new();
            _tokenServiceStub = new();

            _tokenServiceStub.Setup(service => service.GetIdFromToken(It.IsAny<ClaimsPrincipal>()))
                .Returns(RandomGeneratorUtils.GenerateValidNumber(1));
        }

        [Fact]
        public async Task List_WithExistingOrUnexistingAccount_ReturnsTransactionList()
        {
            // Arrange
            var transactionList = new[] { _returnListTransactionDtoFactory.GetInstance() };
            _transactionServiceStub.Setup(service => service.List(It.IsAny<long>(), It.IsAny<ListTransactionDto>()))
                .ReturnsAsync(transactionList);

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.List(It.IsAny<ListTransactionDto>());
            var okObjectResult = result.Result as OkObjectResult;

            // Assert
            okObjectResult.Value.Should().BeEquivalentTo(transactionList);
        }

        [Fact]
        public async Task Deposit_WithUnexistingAccount_ReturnsNotFound()
        {
            // Arrange
            _transactionServiceStub.Setup(service => service.Deposit(It.IsAny<DepositTransactionDto>()))
                .Throws(new AccountNotFoundException());

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Deposit(It.IsAny<DepositTransactionDto>());
            var notFoundObjectResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]
        public async Task Deposit_WithExistingAccount_ReturnsTransactionReferenceId()
        {
            // Arrange
            var transaction = _returnTransactionDtoFactory.GetInstance();

            _transactionServiceStub.Setup(service => service.Deposit(It.IsAny<DepositTransactionDto>()))
                .ReturnsAsync(transaction);

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Deposit(It.IsAny<DepositTransactionDto>());
            var createdObjectResult = result.Result as CreatedAtActionResult;

            // Assert
            createdObjectResult.Value.Should().BeEquivalentTo(transaction);
        }

        [Fact]
        public async Task Withdraw_WithInsufficientBalance_ReturnsForbidden()
        {
            // Arrange
            _transactionServiceStub.Setup(service => service.Withdraw(It.IsAny<long>(), It.IsAny<WithdrawTransactionDto>()))
                .Throws(new InsufficientBalanceException());

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Withdraw(It.IsAny<WithdrawTransactionDto>());
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task Withdraw_WithSufficientBalance_ReturnsTransactionReferenceId()
        {
            // Arrange
            var transaction = _returnTransactionDtoFactory.GetInstance();

            _transactionServiceStub.Setup(service => service.Withdraw(It.IsAny<long>(), It.IsAny<WithdrawTransactionDto>()))
                .ReturnsAsync(transaction);

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Withdraw(It.IsAny<WithdrawTransactionDto>());
            var createdObjectResult = result.Result as CreatedAtActionResult;

            // Assert
            createdObjectResult.Value.Should().BeEquivalentTo(transaction);
        }

        [Fact]
        public async Task Transfer_WithUnexistingAccount_ReturnsNotFound()
        {
            // Arrange
            _transactionServiceStub.Setup(service => service.Transfer(It.IsAny<long>(), It.IsAny<TransferTransactionDto>()))
                .Throws(new AccountNotFoundException());

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Transfer(It.IsAny<TransferTransactionDto>());
            var notFoundObjectResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]
        public async Task Transfer_WithSameOriginAndDestinationAccounts_ReturnsBadRequest()
        {
            // Arrange
            _transactionServiceStub.Setup(service => service.Transfer(It.IsAny<long>(), It.IsAny<TransferTransactionDto>()))
                .Throws(new InvalidOperationException());

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Transfer(It.IsAny<TransferTransactionDto>());
            var badRequestObjectResult = result.Result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestObjectResult);
        }

        [Fact]
        public async Task Transfer_WithInsufficientBalance_ReturnsForbidden()
        {
            // Arrange
            _transactionServiceStub.Setup(service => service.Transfer(It.IsAny<long>(), It.IsAny<TransferTransactionDto>()))
                .Throws(new InsufficientBalanceException());

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Transfer(It.IsAny<TransferTransactionDto>());
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(objectResult.StatusCode, StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task Transfer_WithValidAccountsAndSufficientBalance_ReturnsTransactionReferenceId()
        {
            // Arrange
            var transaction = _returnTransactionDtoFactory.GetInstance();

            _transactionServiceStub.Setup(service => service.Transfer(It.IsAny<long>(), It.IsAny<TransferTransactionDto>()))
                .ReturnsAsync(transaction);

            var controller = new TransactionController(_tokenServiceStub.Object, _transactionServiceStub.Object);

            // Act
            var result = await controller.Transfer(It.IsAny<TransferTransactionDto>());
            var createdObjectResult = result.Result as CreatedAtActionResult;

            // Assert
            createdObjectResult.Value.Should().BeEquivalentTo(transaction);
        }
    }
}
