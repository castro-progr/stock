using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public static class ReporteInventario
    {
        public static void MostrarReporteInventarioTotal()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=========  Reporte Total del Inventario =========\n");
            Console.WriteLine("Código\tNombre\t\tCategoría\tPrecio\tCantidad\tStock Mínimo");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.ResetColor();

            foreach (var producto in Inventario.ListaProductos)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{producto.Codigo}\t{producto.Nombre,-16}\t{producto.Categoria,-10}\t${producto.Precio}\t{producto.Cantidad}\t\t{producto.StockMinimo}");
                Console.ResetColor();
            }

            Console.WriteLine("\n====================================================");
        }

        public static void GuardarReporteInventarioTotal()
        {
            Directory.CreateDirectory("Reportes");
            string ruta = "Reportes/InventarioTotal.txt";
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                writer.WriteLine("Código\tNombre\tCategoría\tPrecio\tCantidad\tStockMinimo");
                foreach (var producto in Inventario.ListaProductos)
                {
                    writer.WriteLine($"{producto.Codigo}\t{producto.Nombre}\t{producto.Categoria}\t{producto.Precio}\t{producto.Cantidad}\t{producto.StockMinimo}");
                }
            }
            Console.WriteLine($" Archivo guardado: {ruta}");
        }
    }
}
