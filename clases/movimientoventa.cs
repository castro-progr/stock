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
        public List<DetalleProducto> det;
        public decimal TotalFactura;
        internal object Monto;

        public MovimientoVenta(Cliente cliente, List<DetalleProducto> detalles)
        {
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Cliente = cliente;
            det = detalles;
            TotalFactura = CalcularTotal();
        }

        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in det)
                total += item.Subtotal;
            return total;
        }

        public void ImprimirFactura()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n======== FACTURA ========");
            Console.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy}");
            Console.WriteLine($"Cliente: {Cliente.NombreCompleto}");
            Console.WriteLine($"Cédula: {Cliente.Cedula}");
            Console.WriteLine("-------------------------");
            Console.ResetColor();

            foreach (var item in det)
            {
                Console.WriteLine($"{item.NombreProducto} x{item.Cantidad} = ${item.Subtotal}");
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"TOTAL: ${TotalFactura}");
            Console.WriteLine("=============================\n");
            Console.ResetColor();
        }
    }
}