using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Entidades
{
    public class ECliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public int idTipoCliente { get; set; }
        //public ETiposClientes tipoCliente { get; set; }
    }
}
