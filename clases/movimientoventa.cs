using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public class MovimientoVenta
    {
        public string Fecha;
        public Cliente Cliente;
        public List<DetalleProducto> DetalleProductos;
        public decimal TotalFactura;

        public MovimientoVenta(Cliente cliente, List<DetalleProducto> detalles)
        {
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Cliente = cliente;
            DetalleProductos = detalles;
            TotalFactura = CalcularTotal();
        }

        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in DetalleProductos)
                total += item.Subtotal;
            return total;
        }

        public void ImprimirFactura()
        {
            Console.WriteLine("========== FACTURA ==========");
            Console.WriteLine($"Fecha: {Fecha}");
            Console.WriteLine($"Cliente: {Cliente.ObtenerNombreCompleto()}");
            Console.WriteLine($"Cédula: {Cliente.Cedula}");
            Console.WriteLine("-----------------------------");
            foreach (var item in DetalleProductos)
                Console.WriteLine($"{item.NombreProducto} x{item.Cantidad} = ${item.Subtotal}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"TOTAL: ${TotalFactura}");
            Console.WriteLine("=============================\n");
        }
    }
}
