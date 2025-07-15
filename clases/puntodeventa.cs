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
            Console.WriteLine("====== Punto de Venta ======\n");

            // CLIENTE
            Console.Write("Ingrese la cédula del cliente: ");
            string cedula = Console.ReadLine();
            Cliente cliente = InventarioClientes.BuscarPorCedula(cedula);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no registrado. Ingrese sus datos:");
                Console.Write("Nombres: ");
                string nombres = Console.ReadLine();
                Console.Write("Apellidos: ");
                string apellidos = Console.ReadLine();
                cliente = new Cliente(cedula, nombres, apellidos);
            }

            // PRODUCTOS
            List<DetalleProducto> listaDetalles = new List<DetalleProducto>();

            string opcion;
            do
            {
                Console.Write("\nIngrese código del producto: ");
                string codigoProducto = Console.ReadLine();
                Producto producto = Inventario.BuscarPorCodigo(codigoProducto);

                if (producto == null)
                {
                    Console.WriteLine("❌ Producto no encontrado.");
                }
                else
                {
                    Console.Write($"Ingrese cantidad (disponible: {producto.Cantidad}): ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                    {
                        if (cantidad <= producto.Cantidad)
                        {
                            listaDetalles.Add(new DetalleProducto(producto.Codigo, producto.Nombre, cantidad, producto.Precio));
                            producto.Cantidad -= cantidad;
                            Console.WriteLine("✅ Producto agregado a la factura.");
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Stock insuficiente.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Cantidad inválida.");
                    }
                }

                Console.Write("¿Agregar otro producto? (S/N): ");
                opcion = Console.ReadLine().Trim().ToUpper();
            }
            while (opcion == "S");

            // FINALIZAR VENTA
            if (listaDetalles.Count == 0)
            {
                Console.WriteLine("\n⚠️ No se agregó ningún producto. Venta cancelada.");
                return;
            }

            MovimientoVenta venta = new MovimientoVenta(cliente, listaDetalles);
            ControlVentas.RegistrarVenta(venta);
            Console.Clear();
            venta.ImprimirFactura();
        }
    }
}
