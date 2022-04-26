using System;

namespace Mongemini.Persistence.Contracts.Exceptions
{
    public class NotFoundException : PersistenceException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
