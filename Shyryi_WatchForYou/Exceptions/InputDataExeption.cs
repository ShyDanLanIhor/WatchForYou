using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Exceptions
{
    public class InputDataException : Exception
    {
        public InputDataException() { }

        public InputDataException(string message) : base(message) { }

        public InputDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
