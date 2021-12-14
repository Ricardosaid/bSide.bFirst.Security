using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bSide.bFirst.Security.Models
{
    public class HistoricAccess
    {
        public int CardNumber { get; set; }
        public DateTime DateAccess { get; set; }
        public Guid IdIoTDevice { get; set; }
    }
}
