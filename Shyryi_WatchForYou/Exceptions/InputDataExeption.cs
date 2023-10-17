using System;

namespace Shyryi_WatchForYou.Exceptions
{
    public class InputDataException : OwnException
    {
        public InputDataException() { }

        public InputDataException(string message) : base(message) { }

        public InputDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
