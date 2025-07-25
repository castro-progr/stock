using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public class DetalleProducto
    {
        public string CodigoProducto;
        public string NombreProducto;
        public int Cantidad;
        public decimal Subtotal;

        public DetalleProducto(string codigo, string nombre, int cantidad, decimal precio)
        {
            CodigoProducto = codigo;
            NombreProducto = nombre;
            Cantidad = cantidad;
            Subtotal = cantidad * precio;
        }

        public int precio { get; internal set; }
    }
}
