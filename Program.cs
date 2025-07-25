using SpreadsheetLight;
using stock.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml.InkML;


namespace stock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (SLDocument sL = new SLDocument())
            {
                string pathfile = AppDomain.CurrentDomain.BaseDirectory + "clientes.xlsx";
                SLDocument sl = new SLDocument();

                DataTable tabla = new DataTable("Clientes");

                // Define todas las columnas que vas a usar
                tabla.Columns.Add("id", typeof(int));
                tabla.Columns.Add("cedula", typeof(string));
                tabla.Columns.Add("nombre", typeof(string));
                tabla.Columns.Add("apellido", typeof(string));
                tabla.Columns.Add("direccion", typeof(string));
                tabla.Columns.Add("correo", typeof(string));
                tabla.Columns.Add("fecharegistro", typeof(string));

                // Inserta los datos en el mismo orden
                tabla.Rows.Add(1, "0986427536", "Camila", "Cortez", "Martha", "andreamg@gmail.com", "04/08/2025");


                // Guardar el archivo Excel
                sl.ImportDataTable(1, 1, tabla, true);
                sl.SaveAs(pathfile);

                // LEER ARCHIVO DE CLIENTES
                SLDocument clientes = new SLDocument(pathfile);
                int indice = 1; // Comienza en fila 2 (fila 1 son los encabezados)


                while (!string.IsNullOrEmpty(clientes.GetCellValueAsString(indice, 1))) // Cedula en columna 2
                {
                    string cedula = clientes.GetCellValueAsString(indice, 2);
                    string nombre = clientes.GetCellValueAsString(indice, 3);
                    string apellido = clientes.GetCellValueAsString(indice, 4);
                    string direccion = clientes.GetCellValueAsString(indice, 5);
                    string correo = clientes.GetCellValueAsString(indice, 6);
                    DateTime fechaRegistro = clientes.GetCellValueAsDateTime(indice, 7);


                   

                    indice++;
                }


                CargarInventarioInicial();
                string opcion = "";

                do
                {
                    Console.Clear();


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("========= MiniStock - Sistema de Inventario =========");
                    Console.ResetColor();


                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Registrar nuevo producto");
                    Console.WriteLine("2. Mostrar todos los productos");
                    Console.WriteLine("3. Buscar producto por código");
                    Console.WriteLine("4. Punto de venta");
                    Console.WriteLine("5. Reporte mensual de ventas");
                    Console.WriteLine("6. Reporte por cliente");
                    Console.WriteLine("7. Registrar nuevo cliente");
                    Console.WriteLine("8. Reporte de inventario");
                    Console.WriteLine("9. Salir");
                    Console.ResetColor();


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=====================================================");
                    Console.ResetColor();


                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Seleccione una opción: ");
                    Console.ResetColor();

                    opcion = Console.ReadLine();
                    Console.Clear();

                    switch (opcion)
                    {
                        case "1":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(" Registro de nuevo producto");
                            Console.ResetColor();

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

                            pathfile = AppDomain.CurrentDomain.BaseDirectory + "inventario.xlsx";

                            if (ProductoYaExisteEnExcel(codigo, pathfile))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" El producto con ese código ya existe. No será registrado.");
                            }
                            else
                            {
                                Producto nuevo = new Producto(codigo, nombre, categoria, precio, cantidad, minimo);
                                Inventario.AgregarProducto(nuevo);

                                SLDocument excel = new SLDocument(pathfile);
                                int nuevaFila = excel.GetWorksheetStatistics().NumberOfRows + 1;

                                excel.SetCellValue(nuevaFila, 1, nuevaFila - 1);
                                excel.SetCellValue(nuevaFila, 2, nuevo.Codigo);
                                excel.SetCellValue(nuevaFila, 3, nuevo.Nombre);
                                excel.SetCellValue(nuevaFila, 4, nuevo.Categoria);
                                excel.SetCellValue(nuevaFila, 5, nuevo.Precio);
                                excel.SetCellValue(nuevaFila, 6, nuevo.Cantidad);
                                excel.SetCellValue(nuevaFila, 7, nuevo.StockMinimo);

                                excel.SaveAs(pathfile);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(" Producto guardado exitosamente.");
                            }
                            Console.ResetColor();
                            Console.ReadLine();
                            break;


                        case "2":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(" Mostrando todos los productos...");
                            Console.ResetColor();
                            Inventario.ImprimirProductos();
                            break;

                        case "3":
                            Console.Write(" Ingrese el código del producto: ");
                            string buscar = Console.ReadLine();
                            Producto producto = Inventario.BuscarPorCodigo(buscar);
                            if (producto != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                producto.Imprimir();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" Producto no encontrado.");
                            }
                            Console.ResetColor();
                            Console.ReadLine();
                            break;

                        case "4":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" Iniciando punto de venta...");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(" Registro de nuevo cliente:");
                            Console.ResetColor();

                            Console.Write("Cedula: ");
                            string cedula = Console.ReadLine();
                            Console.Write("Nombre: ");
                            string nombres = Console.ReadLine();
                            Console.Write("Apellido: ");
                            string apellido = Console.ReadLine();

                            Console.Write("Dirección: ");
                            string direccioncliente = Console.ReadLine();
                            Console.Write("Correo electrónico: ");
                            string correocliente = Console.ReadLine();
                            Console.Write("Fecha de registro (dd/MM/yyyy): ");
                            DateTime fechaRegistro = DateTime.Parse(Console.ReadLine());

                            Cliente nuevoCliente = new Cliente(cedula, nombres, apellido, direccioncliente, correocliente, fechaRegistro);

                            // Guardar en Excel
                            string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "clientes.xlsx";

                            SLDocument documento = new SLDocument(rutaArchivo);
                            int fila = documento.GetWorksheetStatistics().NumberOfRows + 1;

                            documento.SetCellValue(fila, 1, fila);
                            documento.SetCellValue(fila, 2, nuevoCliente.Cedula);
                            documento.SetCellValue(fila, 3, nuevoCliente.Nombres);
                            documento.SetCellValue(fila, 4, nuevoCliente.Apellidos);
                            documento.SetCellValue(fila, 5, nuevoCliente.Direccion);
                            documento.SetCellValue(fila, 6, nuevoCliente.Correo);
                            documento.SetCellValue(fila, 7, nuevoCliente.FechaRegistro.ToString("dd/MM/yyyy"));

                            documento.SaveAs(rutaArchivo);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" Cliente guardado correctamente en el archivo Excel.");
                            Console.ResetColor();

                            // Inicia el proceso de venta
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
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Registro de nuevo cliente");

                            Console.Write("Cédula: ");
                            string ced = Console.ReadLine();
                            Console.Write("Nombre: ");
                            string nom = Console.ReadLine();
                            Console.Write("Apellido: ");
                            string ape = Console.ReadLine();

                            // Cambia los nombres de las variables para evitar duplicados
                            Console.Write("Direccion ");
                            string direccionCliente = Console.ReadLine();
                            Console.Write("Correo electrónico: ");
                            string correoCliente = Console.ReadLine();

                            Console.Write("Fecha de registro (dd/mm/yyyy): ");
                            DateTime fechaRegistroCliente = DateTime.Parse(Console.ReadLine());

                            Cliente nuevoCliente7 = new Cliente(ced, nom, ape, direccionCliente, correoCliente, fechaRegistroCliente);

                            string pathFile = AppDomain.CurrentDomain.BaseDirectory + "clientes.xlsx";

                            if (ClienteYaExisteEnExcel(ced, pathFile))
                            {
                                // Puedes comentar o eliminar este bloque si no quieres mostrar mensaje
                                // Console.ForegroundColor = ConsoleColor.Red;
                                // Console.WriteLine("El cliente con esa cédula ya existe. No será registrado.");
                                // Console.ResetColor();
                            }
                            else
                            {
                                SLDocument excel = new SLDocument(pathFile);
                                int nuevaFila = excel.GetWorksheetStatistics().NumberOfRows + 1;

                                // Asignación de datos en columnas
                                excel.SetCellValue(nuevaFila, 1, nuevaFila);
                                excel.SetCellValue(nuevaFila, 2, nuevoCliente7.Cedula);
                                excel.SetCellValue(nuevaFila, 3, nuevoCliente7.Nombres);
                                excel.SetCellValue(nuevaFila, 4, nuevoCliente7.Apellidos);
                                excel.SetCellValue(nuevaFila, 5, nuevoCliente7.Direccion);
                                excel.SetCellValue(nuevaFila, 6, nuevoCliente7.Correo);
                                excel.SetCellValue(nuevaFila, 7, nuevoCliente7.FechaRegistro.ToString("dd/MM/yyyy"));

                                excel.SaveAs(pathFile);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Cliente guardado exitosamente.");
                                Console.ResetColor();
                            }

                            Console.ReadLine();
                            break;



                        case "8":
                            ReporteInventario.MostrarReporteInventarioTotal();
                            Console.Write("¿Guardar en archivo? (S/N): ");
                            if (Console.ReadLine().Trim().ToUpper() == "S")
                                ReporteInventario.GuardarReporteInventarioTotal();
                            Console.ReadLine();
                            break;

                        case "9":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(" Gracias por usar MiniStock. ¡Hasta pronto!");
                            Console.ResetColor();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Opción no válida.");
                            Console.ResetColor();
                            break;
                    }

                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                }
                while (opcion != "9");
            }
        }

         public static void CargarInventarioInicial()
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





            string pathfile = AppDomain.CurrentDomain.BaseDirectory + "inventario.xlsx";
            SLDocument osldocument = new SLDocument();
            DataTable table = new DataTable("Productos");

            ///columnas inventario
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("Código", typeof(string));
            table.Columns.Add("nombre", typeof(string));
            table.Columns.Add("categoria", typeof(string));
            table.Columns.Add("precio", typeof(decimal));
            table.Columns.Add("cantidad", typeof(int));
            table.Columns.Add("stock minimo", typeof(int));

            ///registro de productos
            table.Rows.Add(1, "P001", "Camisa Deportiva Hombre", "Ropa", 15.99m, 25, 5);
            table.Rows.Add(2, "P002", "Camisa Deportiva Mujer", "Ropa", 16.49m, 30, 5);
            table.Rows.Add(3, "P003", "Chompa Térmica", "Ropa", 32.99m, 10, 2);
            table.Rows.Add(4, "P004", "Pulsera de Silicona", "Accesorios", 2.50m, 50, 10);
            table.Rows.Add(5, "P005", "Cadena de Acero", "Accesorios", 8.90m, 20, 5);
            table.Rows.Add(6, "P006", "Rodillera FlexPro", "Protección", 12.75m, 15, 3);
            table.Rows.Add(7, "P007", "Muñequera Deportiva", "Protección", 6.40m, 25, 5);
            table.Rows.Add(8, "P008", "Camisa DryFit Edición Limitada", "Ropa", 19.99m, 8, 2);
            table.Rows.Add(9, "P009", "Chompa Impermeable Premium", "Ropa", 45.00m, 5, 1);
            table.Rows.Add(10, "P010", "Set de Pulseras Personalizadas", "Accesorios", 11.25m, 12, 2);



            osldocument.ImportDataTable(1, 1, table, true);
            osldocument.SaveAs(pathfile);


            ///LEER ARCHIVO DE INVENTARIO


            int indice = 1;
            SLDocument inventario = new SLDocument(pathfile);
            while (!string.IsNullOrEmpty(inventario.GetCellValueAsString(indice, 1)))
            {
                if (indice == 1)
                {
                    string enc1 = inventario.GetCellValueAsString(indice, 1);
                    string enc2 = inventario.GetCellValueAsString(indice, 2);
                    string enc3 = inventario.GetCellValueAsString(indice, 3);
                    string enc4 = inventario.GetCellValueAsString(indice, 4);
                    string enc5 = inventario.GetCellValueAsString(indice, 5);
                    string enc6 = inventario.GetCellValueAsString(indice, 6);
                    string enc7 = inventario.GetCellValueAsString(indice, 7);
                    string enc8 = inventario.GetCellValueAsString(indice, 8);
                    string enc9 = inventario.GetCellValueAsString(indice, 9);
                    string enc10 = inventario.GetCellValueAsString(indice, 10);
                }
                else
                {
                    string codigoProducto = inventario.GetCellValueAsString(indice, 2);
                    string nombreProducto = inventario.GetCellValueAsString(indice, 3);
                    string categoriaProducto = inventario.GetCellValueAsString(indice, 4);
                    decimal precioProducto = inventario.GetCellValueAsDecimal(indice, 5);
                    int cantidadProducto = inventario.GetCellValueAsInt32(indice, 6);
                    int minimoProducto = inventario.GetCellValueAsInt32(indice, 7);
                    Inventario.AgregarProducto(new Producto(codigoProducto, nombreProducto, categoriaProducto, precioProducto, cantidadProducto, minimoProducto));
                }
                indice++;

            }


         }



        static bool ProductoYaExisteEnExcel(string codigoBuscado, string pathExcel)
        {
            SLDocument excel = new SLDocument(pathExcel);
            int fila = 2;

            while (!string.IsNullOrEmpty(excel.GetCellValueAsString(fila, 2)))
            {
                string codigoActual = excel.GetCellValueAsString(fila, 2).Trim().ToUpper();
                if (codigoActual == codigoBuscado.Trim().ToUpper())
                    return true;

                fila++;
            }
            return false;
        }

        public static bool ClienteYaExisteEnExcel(string cedula, string path)
        {
            if (!File.Exists(path)) return false;

            SLDocument excel = new SLDocument(path);
            int filas = excel.GetWorksheetStatistics().NumberOfRows;

            for (int i = 2; i <= filas; i++) // Suponiendo encabezado en la fila 1
            {
                string cedulaExcel = excel.GetCellValueAsString(i, 2);
                if (cedulaExcel == cedula) return true;
            }

            return false;
        }





    }


}
           



    




