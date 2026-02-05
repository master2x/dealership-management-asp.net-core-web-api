using System.Net.NetworkInformation;
using dealership_api.Models;
using DealershipApp.Console.Models;
using dealership_api.Dtos.ClientesDtos;


namespace dealership_api.Services
{
    public class ClienteService
    {
        private static List<Cliente> clientes = new();

        public List<Cliente> ObtenerTodosClientes()
        {
            return clientes;
        }       
        public Cliente ObtenerClienteId(int id)
        {
            return clientes.FirstOrDefault(c => c.IdCliente == id);
        }

        public Cliente CrearCliente(CrearClienteDTO dto)
        {     if (string.IsNullOrWhiteSpace(dto.NombreCliente) ||
                string.IsNullOrWhiteSpace(dto.ApellidoCliente) ||
                string.IsNullOrWhiteSpace(dto.DireccionCliente) ||
                string.IsNullOrWhiteSpace(dto.CorreoCliente))
                return null;
            if (!dto.CorreoCliente.Contains("@"))
                return null;

            int nuevoId = clientes.Any()
      ? clientes.Max(v => v.IdCliente) + 1
      : 1;

            var cliente = new Cliente

                {
                IdCliente = nuevoId,
                NombreCliente = dto.NombreCliente,
                ApellidoCliente = dto.ApellidoCliente,
                DireccionCliente = dto.DireccionCliente,
                CorreoCliente = dto.CorreoCliente,
                TelefonoCliente = dto.TelefonoCliente
            };

            clientes.Add(cliente);          
           return cliente;
        }

        public bool EliminarCliente(int id)
        {
            var cliente = ObtenerClienteId(id);
            if (cliente == null) return false;
            clientes.Remove(cliente);
            return true;
        }

        public bool ActualizarCliente(int id, ActualizarTodoCliente clienteActualizado)
        {
            var clienteExistente = ObtenerClienteId(id);

            if (clienteExistente == null)
                return false;

            if (string.IsNullOrWhiteSpace(clienteActualizado.NombreCliente) ||
            string.IsNullOrWhiteSpace(clienteActualizado.ApellidoCliente) ||
            string.IsNullOrWhiteSpace(clienteActualizado.DireccionCliente)) 
                    return false;

            if (clienteActualizado.TelefonoCliente <= 0)
                return false;

            clienteExistente.NombreCliente = clienteActualizado.NombreCliente; // El cliente existente se actualiza con los nuevos valores
            clienteExistente.ApellidoCliente = clienteActualizado.ApellidoCliente;
            clienteExistente.DireccionCliente = clienteActualizado.DireccionCliente;
            clienteExistente.TelefonoCliente = clienteActualizado.TelefonoCliente;

            return true;
        }

        public bool ActualizarClientePATCH(int id, ActualizarCamposCliente dto)
        {
            var cliente = ObtenerClienteId(id);
            if (cliente == null)
                return false;

            if (dto.NombreCliente != null)
                cliente.NombreCliente = dto.NombreCliente;

            if (dto.ApellidoCliente != null)
                cliente.ApellidoCliente = dto.ApellidoCliente;

            if (dto.DireccionCliente != null)
                cliente.DireccionCliente = dto.DireccionCliente;

            if (dto.TelefonoCliente.HasValue && dto.TelefonoCliente > 0)// Verificar que si tenga valor y sea mayor a 0
                cliente.TelefonoCliente = dto.TelefonoCliente.Value;

            return true;
        }
    }
}
