using stock.clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stock.clases
{
    public static class Inventario
    {
        public static List<Producto> ListaProductos = new List<Producto>();

        public static void AgregarProductoDesdeConstructor(Producto producto)
        {
            if (!ListaProductos.Any(p => p.Codigo == producto.Codigo))
                ListaProductos.Add(producto);
            else
                Console.WriteLine("⚠️ Producto duplicado, no se agregó.");
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
            foreach (var producto in ListaProductos)
                producto.Imprimir();
        }
    }
}
