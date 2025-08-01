﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public static class ReporteVentas
    {
        public static void MostrarReporteMensual(string mes, string año)
        {
            // 🛠️ 1. Validar que el mes y año sean correctos
            var ventas = ControlVentas.ObtenerVentasDelMes(mes, año);
            decimal total = 0;

            Console.WriteLine($" Reporte mensual {mes}/{año}");
            foreach (var venta in ventas)
            {
                Console.WriteLine($"{venta.Fecha} | {venta.Cliente.NombreCompleto} | Total: ${venta.TotalFactura}");
                total += venta.TotalFactura;
            }
            Console.WriteLine($"TOTAL DEL MES: ${total}\n");
        }

        public static void MostrarReportePorCliente(string cedula, string mes, string año)
        {
            // 🛠️ 1. Validar que el cliente exista
            var ventas = ControlVentas.ObtenerVentasPorCliente(cedula, mes, año);
            decimal total = 0;

            foreach (var venta in ventas)
            {
                Console.WriteLine($"Fecha: {venta.Fecha} | Total: ${venta.TotalFactura}");
                foreach (var item in venta.det)
                    Console.WriteLine($" - {item.NombreProducto} x{item.Cantidad} = ${item.Subtotal}");
                total += venta.TotalFactura;
            }
            Console.WriteLine($"TOTAL COMPRADO: ${total}\n");
        }

        public static void GuardarReporteMensual(string mes, string año)
        {
            // 🛠️ 1. Validar que el mes y año sean correctos
            var ventasDelMes = ControlVentas.ObtenerVentasDelMes(mes, año);
            decimal total = 0;

            // 🛠️ 1. Ruta de carpeta y archivo
            string carpeta = "Reportes";
            string archivo = $"reporte_mensual_{mes}_{año}.txt";
            string ruta = Path.Combine(carpeta, archivo);

            // 🧱 2. Crear carpeta si no existe
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            // 📄 3. Escribir contenido al archivo
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                writer.WriteLine($" Reporte general de ventas: {mes}/{año}");
                writer.WriteLine("--------------------------------------------------");

                foreach (var venta in ventasDelMes)
                {
                    writer.WriteLine($"Fecha: {venta.Fecha} | Cliente: {venta.Cliente.NombreCompleto} | Total: ${venta.TotalFactura}");
                    foreach (var item in venta.det)
                    {
                        writer.WriteLine($" - {item.NombreProducto} x{item.Cantidad} = ${item.Subtotal}");
                    }
                    writer.WriteLine("");
                    total += venta.TotalFactura;
                }

                writer.WriteLine("==================================================");
                writer.WriteLine($" TOTAL ACUMULADO DEL MES: ${total}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Reporte mensual guardado en archivo: {ruta}");
            Console.ResetColor();
        }


        public static void GuardarReportePorCliente(string cedula, string mes, string año)
        {
            // 🛠️ 1. Validar que el cliente exista
            var ventasCliente = ControlVentas.ObtenerVentasPorCliente(cedula, mes, año);
            decimal total = 0;


            // 🧱 2. Verificar si hay ventas del cliente
            string ruta = $"Reportes/reporte_cliente_{cedula}_{mes}_{año}.txt";
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                writer.WriteLine($" Reporte de compras del cliente {cedula} en {mes}/{año}");
                writer.WriteLine("--------------------------------------------------");

                foreach (var venta in ventasCliente)
                {
                    writer.WriteLine($"Fecha: {venta.Fecha} | Total: ${venta.TotalFactura}");
                    foreach (var item in venta.det)
                    {
                        writer.WriteLine($" - {item.NombreProducto} x{item.Cantidad} = ${item.Subtotal}");
                    }
                    writer.WriteLine("");
                    total += venta.TotalFactura;
                }

                writer.WriteLine("==================================================");
                writer.WriteLine($" TOTAL COMPRADO POR EL CLIENTE: ${total}");
            }

            Console.WriteLine($" Reporte del cliente guardado en archivo: {ruta}");
        }

    }
}
