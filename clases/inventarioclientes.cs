using stock.clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stock.clases
{
    public static class InventarioClientes
    {
        public static List<Cliente> ListaClientes = new List<Cliente>();

        public static void RegistrarCliente(Cliente cliente)
        {
            if (!ListaClientes.Any(c => c.Cedula == cliente.Cedula))
            {
                ListaClientes.Add(cliente);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cliente registrado correctamente.");
                Console.ResetColor();
            }
           
        }

        

        public static Cliente BuscarPorCedula(string cedula)
        {
            return ListaClientes.FirstOrDefault(c => c.Cedula == cedula);
        }
        public static void MostrarClientes()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====== Lista de Clientes ======\n");
            Console.ResetColor();
            foreach (var cliente in ListaClientes)
            {
                cliente.MostrarResumen();
                Console.WriteLine("-----------------------------");
            }
        }
    }
}

        