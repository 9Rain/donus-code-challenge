using System;

namespace IBank.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(
            string message = "Client not found"
        ) : base(message)
        {
        }

        public ClientNotFoundException(
            Exception inner,
            string message = "Client not found"
        ) : base(message, inner)
        {
        }
    }
}
