using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DataAccess.Data.Account;
using DataAccess.Data.Transaction;
using DataAccess.Models;
using FluentAssertions;
using IBank.Dtos.Transaction;
using IBank.Dtos.Transaction.Addresse;
using IBank.Exceptions;
using IBank.Services.Account;
using IBank.Services.Token;
using IBank.Services.Transaction;
using IBank.UnitTests.Factories.AccountModel;
using IBank.UnitTests.Factories.ReturnListTransactionDto;
using IBank.UnitTests.Factories.ReturnTransactionDto;
using IBank.UnitTests.Factories.TransactionModel;
using IBank.UnitTests.Utils;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace IBank.UnitTests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionData> _transactionData;
        private readonly Mock<IAccountData> _accountData;
        private readonly Mock<IAccountService> _accountService;
        private readonly Mock<IMapper> _mapper;

        private readonly IReturnListTransactionDtoFactory _returnListTransactionDtoFactory;
        private readonly ITransactionModelFactory _transactionModelFactory;
        private readonly IAccountModelFactory _accountModelFactory;

        public TransactionServiceTests(
            IReturnListTransactionDtoFactory returnListTransactionDtoFactory,
            ITransactionModelFactory transactionModelFactory,
            IAccountModelFactory accountModelFactory
        )
        {
            _transactionData = new();
            _accountData = new();
            _accountService = new();
            _mapper = new();

            _returnListTransactionDtoFactory = returnListTransactionDtoFactory;
            _transactionModelFactory = transactionModelFactory;
            _accountModelFactory = accountModelFactory;
        }

        [Fact]
        public async Task List_WithValidParameters_ReturnsListTransactionDto()
        {
            // Arrange
            var transactionModel = _transactionModelFactory.GetInstance();
            var transactionDto = _returnListTransactionDtoFactory.GetInstance();
            var transactionModelList = new[] { transactionModel };

            _transactionData.Setup(data => data.List(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(transactionModelList);

            _mapper.Setup(map => map.Map<ReturnListTransactionDto>(It.IsAny<TransactionModel>()))
                .Returns(transactionDto);

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act
            var result = await service.List(It.IsAny<long>(), new ListTransactionDto());

            // Assert
            Assert.Equal(result.Count(), transactionModelList.Count());
        }

        [Fact]
        public async Task Deposit_WithNoExistingAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            _accountService.Setup(service => service.GetForTransaction(It.IsAny<AddresseTransactionDto>()))
                .ThrowsAsync(new AccountNotFoundException());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.Deposit(new DepositTransactionDto()));
        }

        [Fact]
        public async Task Deposit_WithExistingAccount_ReturnsReturnTransactionDto()
        {
            // Arrange
            var account = _accountModelFactory.GetInstance();
            var transaction = _transactionModelFactory.GetInstance();

            _accountService.Setup(service => service.GetForTransaction(It.IsAny<AddresseTransactionDto>()))
                .ReturnsAsync(account);

            _transactionData.Setup(data => data.Deposit(account, It.IsAny<string>(), It.IsAny<decimal>()))
                .ReturnsAsync(transaction);

            _mapper.Setup(map => map.Map<ReturnTransactionDto>(transaction))
                .Returns(new ReturnTransactionDto());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act
            var result = await service.Deposit(new DepositTransactionDto());

            // Assert
            Assert.IsType<ReturnTransactionDto>(result);
        }

        [Fact]
        public async Task Withdraw_WithNoExistingAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ThrowsAsync(new AccountNotFoundException());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.Withdraw(It.IsAny<long>(), It.IsAny<WithdrawTransactionDto>()));
        }

        [Fact]
        public async Task Withdraw_WithInsufficientBalance_ThrowsInsufficientBalanceException()
        {
            // Arrange
            var withdraw = new WithdrawTransactionDto();
            withdraw.Amount = 2;

            var account = _accountModelFactory.GetInstance();
            account.Balance = 1;

            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(account);

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<InsufficientBalanceException>(() => service.Withdraw(It.IsAny<long>(), withdraw));
        }

        [Fact]
        public async Task Withdraw_WithValidParameters_ReturnsReturnTransactionDto()
        {
            // Arrange
            var transaction = _transactionModelFactory.GetInstance();

            var withdraw = new WithdrawTransactionDto();
            withdraw.Amount = 1;

            var account = _accountModelFactory.GetInstance();
            account.Balance = 1;

            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(account);

            _transactionData.Setup(data => data.Withdraw(account, It.IsAny<string>(), It.IsAny<decimal>()))
                .ReturnsAsync(transaction);

            _mapper.Setup(map => map.Map<ReturnTransactionDto>(transaction))
                .Returns(new ReturnTransactionDto());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act
            var result = await service.Withdraw(It.IsAny<long>(), new WithdrawTransactionDto());

            // Assert
            Assert.IsType<ReturnTransactionDto>(result);
        }

        [Fact]
        public async Task Transfer_WithNoExistingOriginAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ThrowsAsync(new AccountNotFoundException());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.Transfer(It.IsAny<long>(), It.IsAny<TransferTransactionDto>()));
        }

        [Fact]
        public async Task Transfer_WithNoExistingDestinationAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            var transfer = new TransferTransactionDto();
            transfer.Amount = 1;

            var origin = _accountModelFactory.GetInstance();
            origin.Balance = transfer.Amount;

            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(origin);

            _accountService.Setup(service => service.GetForTransaction(It.IsAny<AddresseTransactionDto>()))
                .ThrowsAsync(new AccountNotFoundException());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<AccountNotFoundException>(() => service.Transfer(It.IsAny<long>(), transfer));
        }

        [Fact]
        public async Task Transfer_WithOriginInsufficientBalance_ThrowsInsufficientBalanceException()
        {
            // Arrange
            var transfer = new TransferTransactionDto();
            transfer.Amount = 1;

            var origin = _accountModelFactory.GetInstance();
            origin.Balance = 0;

            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(origin);

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<InsufficientBalanceException>(() => service.Transfer(It.IsAny<long>(), transfer));
        }

        [Fact]
        public async Task Transfer_WithSameOriginAndDestinationAccounts_ThrowsInvalidOperationException()
        {
            // Arrange
            var transfer = new TransferTransactionDto();
            transfer.Amount = 1;

            var origin = _accountModelFactory.GetInstance();
            origin.Balance = 1;

            var destination = _accountModelFactory.GetInstance();
            destination.ClientId = 1L;

            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(origin);

            _accountService.Setup(service => service.GetForTransaction(It.IsAny<AddresseTransactionDto>()))
                .ReturnsAsync(destination);

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.Transfer(1L, transfer));
        }

        [Fact]
        public async Task Transfer_WithValidParameters_ReturnsReturnTransactionDto()
        {
            // Arrange
            var transaction = _transactionModelFactory.GetInstance();

            var transfer = new TransferTransactionDto();
            transfer.Amount = 1;

            var origin = _accountModelFactory.GetInstance();
            origin.Balance = 1;

            var destination = _accountModelFactory.GetInstance();
            destination.ClientId = 2L;

            _accountData.Setup(data => data.GetByClientId(It.IsAny<long>()))
                .ReturnsAsync(origin);

            _transactionData.Setup(data => data.Transfer(origin, destination, It.IsAny<string>(), It.IsAny<decimal>()))
                .ReturnsAsync(transaction);

            _accountService.Setup(service => service.GetForTransaction(It.IsAny<AddresseTransactionDto>()))
                .ReturnsAsync(destination);

            _mapper.Setup(map => map.Map<ReturnTransactionDto>(transaction))
                .Returns(new ReturnTransactionDto());

            var service = new TransactionService(
                _transactionData.Object, _accountData.Object,
                _accountService.Object, _mapper.Object
            );

            // Act
            var result = await service.Transfer(1L, transfer);

            // Assert
            Assert.IsType<ReturnTransactionDto>(result);
        }
    }
}
