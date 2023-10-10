using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou_Server.Models
{
    [Serializable]
    public class RequestEntity
    {
        public string Type { get; set; }
        public object Data { get; set; }
    }
}
