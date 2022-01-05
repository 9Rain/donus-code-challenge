namespace IBank.Dtos.Account
{
    public class ReturnAccountBalanceDto
    {
        public ReturnAccountBalanceDto(decimal balance)
        {
            Balance = balance;
        }

        public decimal Balance { get; set; }
    }
}
