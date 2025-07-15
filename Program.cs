using stock.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CargarInventarioInicial();
            string opcion = "";

            do
            {
                Console.Clear();
                Console.WriteLine("========= MiniStock - Sistema de Inventario =========");
                Console.WriteLine("1. Registrar nuevo producto");
                Console.WriteLine("2. Mostrar todos los productos");
                Console.WriteLine("3. Buscar producto por código");
                Console.WriteLine("4. Punto de venta");
                Console.WriteLine("5. Reporte mensual de ventas");
                Console.WriteLine("6. Reporte por cliente");
                Console.WriteLine("7. Registrar nuevo cliente");
                Console.WriteLine("8. Reporte de compras por cliente");
                Console.WriteLine("9. Salir");
                Console.WriteLine("=====================================================");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();
                Console.Clear();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Código: ");
                        string codigo = Console.ReadLine();
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Categoría: ");
                        string categoria = Console.ReadLine();
                        Console.Write("Precio: ");
                        decimal precio = decimal.Parse(Console.ReadLine());
                        Console.Write("Cantidad: ");
                        int cantidad = int.Parse(Console.ReadLine());
                        Console.Write("Stock mínimo: ");
                        int minimo = int.Parse(Console.ReadLine());

                        Producto nuevo = new Producto(codigo, nombre, categoria, precio, cantidad, minimo);
                        Console.ReadLine();
                        break;

                    case "2":
                        Inventario.ImprimirProductos();
                        break;

                    case "3":
                        Console.Write("Ingrese el código del producto: ");
                        string buscar = Console.ReadLine();
                        Producto producto = Inventario.BuscarPorCodigo(buscar);
                        if (producto != null)
                            producto.Imprimir();
                        else
                            Console.WriteLine("❌ Producto no encontrado.");
                        Console.ReadLine();
                        break;

                    case "4":
                        PuntoDeVenta.IniciarVenta();
                        break;

                    case "5":
                        Console.Write("Mes (MM): ");
                        string mesM = Console.ReadLine();
                        Console.Write("Año (AAAA): ");
                        string añoM = Console.ReadLine();
                        ReporteVentas.MostrarReporteMensual(mesM, añoM);
                        Console.Write("¿Guardar en archivo? (S/N): ");
                        if (Console.ReadLine().ToUpper() == "S")
                            ReporteVentas.GuardarReporteMensual(mesM, añoM);
                        Console.ReadLine();
                        break;

                    case "6":
                        Console.Write("Cédula del cliente: ");
                        string cedR = Console.ReadLine();
                        Console.Write("Mes (MM): ");
                        string mesR = Console.ReadLine();
                        Console.Write("Año (AAAA): ");
                        string añoR = Console.ReadLine();
                        ReporteVentas.MostrarReportePorCliente(cedR, mesR, añoR);
                        Console.Write("¿Guardar en archivo? (S/N): ");
                        if (Console.ReadLine().ToUpper() == "S")
                            ReporteVentas.GuardarReportePorCliente(cedR, mesR, añoR);
                        Console.ReadLine();
                        break;

                    case "7":
                        Console.Write("Cédula: ");
                        string ced = Console.ReadLine();
                        Console.Write("Nombre: ");
                        string nom = Console.ReadLine();
                        Console.Write("Apellido: ");
                        string ape = Console.ReadLine();
                        Cliente nuevoCliente = new Cliente(ced, nom, ape);
                        InventarioClientes.RegistrarCliente(nuevoCliente);
                        Console.ReadLine();
                        break;

                    case "8":
                        Console.Write("Cédula del cliente: ");
                        string ced8 = Console.ReadLine();
                        Console.Write("Mes (MM): ");
                        string mes8 = Console.ReadLine();
                        Console.Write("Año (AAAA): ");
                        string año8 = Console.ReadLine();
                        ReporteVentas.MostrarReportePorCliente(ced8, mes8, año8);
                        Console.Write("¿Guardar en archivo? (S/N): ");
                        if (Console.ReadLine().ToUpper() == "S")
                            ReporteVentas.GuardarReportePorCliente(ced8, mes8, año8);
                        Console.ReadLine();
                        break;

                    case "9":
                        Console.WriteLine("👋 Gracias por usar MiniStock. ¡Hasta pronto!");
                        break;

                    default:
                        Console.WriteLine("⚠️ Opción no válida.");
                        break;
                }
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }
            while (opcion != "9");
        }

        static void CargarInventarioInicial()
        {
            Inventario.AgregarProducto(new Producto("P001", "Camisa Deportiva Hombre", "Ropa", 15.99m, 25, 5));
            Inventario.AgregarProducto(new Producto("P002", "Camisa Deportiva Mujer", "Ropa", 16.49m, 30, 5));
            Inventario.AgregarProducto(new Producto("P003", "Chompa Térmica", "Ropa", 32.99m, 10, 2));
            Inventario.AgregarProducto(new Producto("P004", "Pulsera de Silicona", "Accesorios", 2.50m, 50, 10));
            Inventario.AgregarProducto(new Producto("P005", "Cadena de Acero", "Accesorios", 8.90m, 20, 5));
            Inventario.AgregarProducto(new Producto("P006", "Rodillera FlexPro", "Protección", 12.75m, 15, 3));
            Inventario.AgregarProducto(new Producto("P007", "Muñequera Deportiva", "Protección", 6.40m, 25, 5));
            Inventario.AgregarProducto(new Producto("P008", "Camisa DryFit Edición Limitada", "Ropa", 19.99m, 8, 2));
            Inventario.AgregarProducto(new Producto("P009", "Chompa Impermeable Premium", "Ropa", 45.00m, 5, 1));
            Inventario.AgregarProducto(new Producto("P010", "Set de Pulseras Personalizadas", "Accesorios", 11.25m, 12, 2));
        }
    }
}
