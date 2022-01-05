using System;

namespace IBank.Exceptions
{
    public class ClientAlreadyHasAnAccountException : Exception
    {
        public ClientAlreadyHasAnAccountException(
            string message = "Client already has an account"
        ) : base(message)
        {
        }

        public ClientAlreadyHasAnAccountException(
            Exception inner,
            string message = "Client already has an account"
        ) : base(message, inner)
        {
        }
    }
}