using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public static class PuntoDeVenta
    {
        public static void IniciarVenta()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====== Punto de Venta ======\n");
            Console.ResetColor();

            
            

            // CLIENTE
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Ingrese la cédula del cliente: ");
            string cedula = Console.ReadLine();
            Cliente cliente = InventarioClientes.BuscarPorCedula(cedula);
            Console.ResetColor();

            if (cliente == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cliente no registrado. Ingrese sus datos:");

                Console.Write("Nombre: ");
                string nom = Console.ReadLine();
                Console.Write("Apellido: ");
                string ape = Console.ReadLine();
                Console.Write("Direccion ");
                string direccion = Console.ReadLine();
                Console.Write("Correo electrónico: ");
                string correo = Console.ReadLine();

                Console.Write("Fecha de registro (dd/mm/yyyy): ");
                DateTime fechaRegistro = DateTime.Parse(Console.ReadLine());


                Console.ResetColor();
                cliente = new Cliente(cedula, nom, ape);
            }
            List<DetalleProducto> listaDetalles = new List<DetalleProducto>();

            string opcion;
            do
            {
                Console.Write("Ingrese código del producto: ");
                string codigoProducto = Console.ReadLine().Trim();

                Producto producto = Inventario.BuscarPorCodigo(codigoProducto);

                if (producto != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Producto: {producto.Nombre}");
                    Console.WriteLine($"Precio unitario: ${producto.Precio:F2}");
                    Console.WriteLine($"Stock disponible: {producto.Cantidad}");
                    Console.ResetColor();

                    Console.Write($"Ingrese cantidad (disponible: {producto.Cantidad}): ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                    {
                        if (cantidad <= producto.Cantidad)
                        {
                            listaDetalles.Add(new DetalleProducto(producto.Codigo, producto.Nombre, cantidad, producto.Precio));
                            producto.Cantidad -= cantidad;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine(" Producto agregado a la factura.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Stock insuficiente.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(" Cantidad inválida.");
                    }

                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Producto no encontrado.");
                    Console.ResetColor();
                }

            
                

                Console.Write("¿Agregar otro producto? (S/N): ");
                opcion = Console.ReadLine().Trim().ToUpper();
            }
            while (opcion == "S");

            // FINALIZAR VENTA
            if (listaDetalles.Count == 0)
            {
                Console.WriteLine("\n No se agregó ningún producto. Venta cancelada.");
                return;
            }

            MovimientoVenta venta = new MovimientoVenta(cliente, listaDetalles);
            ControlVentas.RegistrarVenta(venta);
            Console.Clear();
            venta.ImprimirFactura();
        }
    }
}