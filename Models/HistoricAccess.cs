using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bSide.bFirst.Security.Models
{

    public class HistoricAccess
    {
        //Valores que voy a obtener del historico de acceso
        public int cardNumber { get; set; }
        public DateTime dateAccess { get; set; }
        public string idIoTDevice { get; set; }
        public string id { get; set; }
        public string partitionKey { get; set; }
        public string eTag { get; set; }
        public DateTime timestamps { get; set; }
    }

}
