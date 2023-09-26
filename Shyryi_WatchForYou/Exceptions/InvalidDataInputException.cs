using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Exceptions
{
    public class InvalidDataInputException : InputDataException
    {
        public InvalidDataInputException() { }

        public InvalidDataInputException(string message) : base(message) { }

        public InvalidDataInputException(string message, Exception innerException) : base(message, innerException) { }
    }
}
