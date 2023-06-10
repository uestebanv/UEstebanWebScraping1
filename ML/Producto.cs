using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Precio { get; set; }
        public string Vendedor { get; set; }
        public List<object> Productos { get; set; }
    }
}
