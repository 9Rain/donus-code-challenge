using IBank.Dtos.Transaction;
using IBank.Exceptions;
using IBank.Services.Token;
using IBank.Services.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBank.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ITransactionService _transactionService;

        public TransactionController(
            ITokenService tokenService,
            ITransactionService transactionService
        )
        {
            _tokenService = tokenService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnListTransactionDto>>> List([FromQuery] ListTransactionDto range)
        {
            var id = long.Parse(_tokenService.GetIdFromToken(User));
            var list = await _transactionService.List(id, range);
            return Ok(list);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("deposit")]
        public async Task<ActionResult<ReturnTransactionDto>> Deposit(DepositTransactionDto deposit)
        {
            try
            {
                var transaction = await _transactionService.Deposit(deposit);
                return CreatedAtAction("List", transaction);
            }
            catch (AccountNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<ActionResult<ReturnTransactionDto>> Withdraw(WithdrawTransactionDto withdraw)
        {
            try
            {
                var id = long.Parse(_tokenService.GetIdFromToken(User));
                var transaction = await _transactionService.Withdraw(id, withdraw);
                return CreatedAtAction("List", transaction);
            }
            catch (InsufficientBalanceException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
        }

        [HttpPost]
        [Route("transfer")]
        public async Task<ActionResult<ReturnTransactionDto>> Transfer(TransferTransactionDto transfer)
        {
            try
            {
                var id = long.Parse(_tokenService.GetIdFromToken(User));
                var transaction = await _transactionService.Transfer(id, transfer);
                return CreatedAtAction("List", transaction);
            }
            catch (AccountNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { e.Message });
            }
            catch (InsufficientBalanceException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
        }
    }
}
