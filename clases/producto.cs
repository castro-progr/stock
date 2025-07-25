using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public class Producto
    {
        private int id;
        public string Codigo;
        public string Nombre;
        public string Categoria;
        public decimal Precio;
        public int Cantidad;
        public int StockMinimo;

        public Producto(string codigo, string nombre, string categoria, decimal precio, int cantidad, int stockMinimo)
        {
            this.id = Inventario.ListaProductos.Count + 1;
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Cantidad = cantidad;
            StockMinimo = stockMinimo;
            Inventario.AgregarProductoDesdeConstructor(this);
        }

        public void Imprimir()
        {
            Console.WriteLine("===================================");
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Código: {Codigo}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Categoría: {Categoria}");
            Console.WriteLine($"Precio: ${Precio}");
            Console.WriteLine($"Cantidad: {Cantidad}");
            Console.WriteLine($"Stock mínimo: {StockMinimo}");
            Console.WriteLine("===================================");
        }
    }
}