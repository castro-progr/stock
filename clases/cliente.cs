using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.clases
{
    public class Cliente
    {
        public string Cedula;
        private string Nombres;
        private string Apellidos;
        private string NombreCompleto;

        public Cliente(string cedula, string nombres, string apellidos)
        {
            Cedula = cedula;
            Nombres = nombres;
            Apellidos = apellidos;
            NombreCompleto = nombres + " " + apellidos;
            InventarioClientes.RegistrarCliente(this);
        }

        public string ObtenerNombreCompleto() => NombreCompleto;

        public void Imprimir()
        {
            Console.WriteLine("===================================");
            Console.WriteLine($"Cédula: {Cedula}");
            Console.WriteLine($"Nombre: {Nombres}");
            Console.WriteLine($"Apellido: {Apellidos}");
            Console.WriteLine($"Nombre Completo: {NombreCompleto}");
            Console.WriteLine("===================================");
        }
    }
}
