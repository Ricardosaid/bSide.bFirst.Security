using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bSide.bFirst.Security.Helpers
{
    /*
     * Definimos una clase para representar el objeto JSON devuelto desde la API
     * de query. Usará esta clase para mostrar una lista de las CardsNumber de repositorio. 
     * Por lo que trabajaremos con un subconjunto de campos de un paquete JSon
     
     */
    public class RepoDes
    {
        public int cardNumber { get; set; } //La convención es que sea mayuscula la primera letra, pero en el JSOn la tenemos así
        public string idIoTDevice { get; set; } // Agregamos un nuevo atributo
    }
}
