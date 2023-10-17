using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Exceptions
{
    public class OwnException : Exception
    {
        public OwnException() { }

        public OwnException(string message) : base(message) { }

        public OwnException(string message, Exception innerException) : base(message, innerException) { }
    }
}
