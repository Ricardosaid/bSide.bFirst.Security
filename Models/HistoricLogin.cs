using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bSide.bFirst.Security.Models
{
    public class HistoricLogin
    {
        public Guid IdIoTDevice { get; set; }
        public DateTime DateAccess { get; set; }
    }
}
