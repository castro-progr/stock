using stock.clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stock.clases
{
    public static class Inventario
    {
        public static List<Producto> ListaProductos = new List<Producto>();

        public static bool AgregarProductoDesdeConstructor(Producto producto)
        {
            string nuevoCodigo = producto.Codigo.Trim().ToUpper();

            bool esDuplicado = ListaProductos.Any(p => p.Codigo.Trim().ToUpper() == nuevoCodigo);

            if (esDuplicado)
            {
                // Silenciar el mensaje, solo ignorar el guardado
                return false;
            }

            ListaProductos.Add(producto);
            return true;
        }

        

        public static void AgregarProducto(Producto producto)
        {
            AgregarProductoDesdeConstructor(producto);
        }

        public static Producto BuscarPorCodigo(string codigo)
        {
            return ListaProductos.FirstOrDefault(p => p.Codigo == codigo);
        }

        public static void ImprimirProductos()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("====== LISTA DE PRODUCTOS ======");
            Console.ResetColor();

            foreach (var producto in ListaProductos)
                producto.Imprimir();
        }
    }
}
        
