using System;

namespace IBank.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(
            string message = "Transaction couldn't be fulfilled due to insufficient balance"
        ) : base(message)
        {
        }

        public InsufficientBalanceException(
            Exception inner,
            string message = "Transaction couldn't be fulfilled due to insufficient balance"
        ) : base(message, inner)
        {
        }
    }
}

