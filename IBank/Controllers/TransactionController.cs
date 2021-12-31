using IBank.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IBank.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        public IActionResult List(DateTime startDate, DateTime endDate)
        {
            // Retornar histórico de transações
            return Ok(new ListTransactionDto());
        }

        [HttpGet]
        [Route("available")]
        public IActionResult Available()
        {
            // Retornar lista de tipos de transação disponíveis e rotas
            return Ok();
        }

        [HttpPost]
        [Route("deposit")]
        public IActionResult Deposit(DepositTransactionDto deposit)
        {
            // Chamar service para depositar
            return Created("", new ReturnTransactionDto());
        }

        [HttpPost]
        [Route("withdraw")]
        public IActionResult WithDraw(WithdrawTransactionDto withdraw)
        {
            // Chamar service para sacar
            return Created("", new ReturnTransactionDto());
        }

        [HttpPost]
        [Route("transfer")]
        public IActionResult Transfer(TransferTransactionDto transfer)
        {
            // Chamar service para transferir
            return Created("", new ReturnTransactionDto());
        }

        [HttpPost]
        [Route("loan")]
        public IActionResult Loan(LoanTransactionDto loan)
        {
            // Chamar service para emprestar
            return Created("", new ReturnTransactionDto());
        }

        [HttpPost]
        [Route("pay")]
        public IActionResult Pay(PayTransactionDto pay)
        {
            // Chamar service para emprestar
            return Created("", new ReturnTransactionDto());
        }
    }
}
