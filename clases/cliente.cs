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
     

        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string Direccion { get; set; }      
        public string Correo { get; set; }

        public DateTime FechaRegistro { get; set; }
        

        public Cliente(string cedula, string nombres, string apellidos, string direccion, string correo, DateTime fechaRegistro)
        {
            this.Cedula = cedula;
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.Direccion = direccion;
            this.Correo = correo;
            this.FechaRegistro = fechaRegistro;
            

            InventarioClientes.RegistrarCliente(this);
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


            Console.ResetColor();
            Console.WriteLine(new string('-', 40));
        }

        



    }

}
