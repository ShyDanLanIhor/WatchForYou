using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Exceptions
{
    public class NullReturnDataExeption : OwnException
    {
        public NullReturnDataExeption() { }

        public NullReturnDataExeption(string message) : base(message) { }

        public NullReturnDataExeption(string message, Exception innerException) : base(message, innerException) { }
    }
}
