using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace stock.clases
{
    public class Cliente
    {
        private string nom;
        private string ape;

        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string Direccion { get; set; }      
        public string Correo { get; set; }

        public DateTime FechaRegistro { get; set; }
        public int PuntosFidelidad { get; set; }

        public Cliente(string cedula, string nombres, string apellidos, string direccion, string correo, DateTime fechaRegistro)
        {
            Cedula = cedula;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Correo = correo;
            FechaRegistro = fechaRegistro;
            PuntosFidelidad = 0;

            InventarioClientes.RegistrarCliente(this);
        }

        public Cliente(string nom, string ape, string direccion, string correo, DateTime fechaRegistro)
        {
            this.nom = nom;
            this.ape = ape;
            Direccion = direccion;
            Correo = correo;
            FechaRegistro = fechaRegistro;
        }

        public Cliente(string cedula, string nombres, string apellidos)
        {
            Cedula = cedula;
            Nombres = nombres;
            Apellidos = apellidos;
        }

        public void MostrarResumen()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" Cliente: {NombreCompleto}");


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Cédula: {Cedula}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Correo: {Correo}");


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" Fecha de registro: {FechaRegistro.ToShortDateString()}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Puntos de fidelidad: {PuntosFidelidad}");

            Console.ResetColor();
            Console.WriteLine(new string('-', 40));
        }

        public void AgregarPuntos(int cantidad)
        {
            PuntosFidelidad += cantidad;


            
            

        }

        internal static object ObtenerNombreCompleto()
        {
            throw new NotImplementedException();
        }
    }
}
