using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Exceptions
{
    public class ExistingDataException : InputDataException
    {
        public ExistingDataException() { }

        public ExistingDataException(string message) : base(message) { }

        public ExistingDataException(string message, Exception innerException) : base(message, innerException) { }
    }

}
