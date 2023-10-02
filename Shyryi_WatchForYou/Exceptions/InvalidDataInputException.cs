using System;

namespace Shyryi_WatchForYou.Exceptions
{
    public class InvalidDataInputException : InputDataException
    {
        public InvalidDataInputException() { }

        public InvalidDataInputException(string message) : base(message) { }

        public InvalidDataInputException(string message, Exception innerException) : base(message, innerException) { }
    }
}
