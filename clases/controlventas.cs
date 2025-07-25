using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public static class ControlVentas
    {
        public static List<MovimientoVenta> VentasRegistradas = new List<MovimientoVenta>();

        public static void RegistrarVenta(MovimientoVenta venta)
        {
            VentasRegistradas.Add(venta);
        }

        public static List<MovimientoVenta> ObtenerVentasDelMes(string mes, string año)
        {
            return VentasRegistradas.Where(v =>
                v.Fecha.Contains($"/{mes}/") && v.Fecha.EndsWith($"/{año}")
            ).ToList();
        }

        public static List<MovimientoVenta> ObtenerVentasPorCliente(string cedula, string mes, string año)
        {
            return VentasRegistradas.Where(v =>
                v.Cliente.Cedula == cedula &&
                v.Fecha.Contains($"/{mes}/") &&
                v.Fecha.EndsWith($"/{año}")
            ).ToList();
        }

        public static void ImprimirVentasConColor(List<MovimientoVenta> ventas)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===================================");
            Console.WriteLine($" TOTAL DE VENTAS: {ventas.Count}");
            Console.WriteLine("===================================");
            Console.ResetColor();

            foreach (var venta in ventas)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-------- VENTA --------");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Fecha: {venta.Fecha}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Cliente: {venta.Cliente.Nombres} {venta.Cliente.Apellidos}"); 

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Monto: ${venta.Monto:F2}");

                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}