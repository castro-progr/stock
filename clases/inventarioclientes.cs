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
                ListaClientes.Add(cliente);
            else
                Console.WriteLine("⚠️ Cliente duplicado.");
        }

        public static Cliente BuscarPorCedula(string cedula)
        {
            return ListaClientes.FirstOrDefault(c => c.Cedula == cedula);
        }
    }
}
