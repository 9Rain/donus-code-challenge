using System;

namespace IBank.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(
            string message = "Account not found"
        ) : base(message)
        {
        }

        public AccountNotFoundException(
            Exception inner,
            string message = "Account not found"
        ) : base(message, inner)
        {
        }
    }
}
