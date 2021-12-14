using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bSide.bFirst.Security.Models
{
    public class Card
    {
        public int CardNumber { get; set; }
        public string Privilege { get; set; }
        public Guid IdBuilding { get; set; }
        public int Trys { get; set; }
        public bool Status { get; set; }

    }
}
