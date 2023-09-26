using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Exceptions
{
    public class ExistingUsernameException : Exception
    {
        public ExistingUsernameException() { }

        public ExistingUsernameException(string message) : base(message) { }

        public ExistingUsernameException(string message, Exception innerException) : base(message, innerException) { }
    }

}
