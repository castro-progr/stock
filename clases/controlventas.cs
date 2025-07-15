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
    }
}
