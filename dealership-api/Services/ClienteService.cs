using System.Net.NetworkInformation;
using dealership_api.Models;
using DealershipApp.Console.Models;
using dealership_api.Dtos.ClientesDtos;
using dealership_api.Data;


namespace dealership_api.Services
{
    public class ClienteService
    {
        private readonly DealershipDbContext _context; // Esto reemplaza la lista en memoria, para poder usar la base de datos

        public ClienteService(DealershipDbContext context) // Cuando alguien cree un ClienteService, pásame un DealershipDbContext y guárdalo
        {
            _context = context;
        }


        public List<Cliente> ObtenerTodosClientes()
        {
            return _context.Clientes.ToList();
        }       
        public Cliente ObtenerClienteId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID invalido");
            return _context.Clientes.Find(id);
        }

        public Cliente CrearCliente(CrearClienteDTO dto)
        {     if (string.IsNullOrWhiteSpace(dto.NombreCliente) ||
                string.IsNullOrWhiteSpace(dto.ApellidoCliente) ||
                string.IsNullOrWhiteSpace(dto.DireccionCliente) ||
                string.IsNullOrWhiteSpace(dto.CorreoCliente))
                throw new ArgumentException("Datos incompletos");
            if (!dto.CorreoCliente.Contains("@"))
                throw new ArgumentException("Correo invalido");

            // Se elimino la creacion del id ya que EF lo crea automaticamente al agregar el cliente a la base de datos

            var cliente = new Cliente

                {
                NombreCliente = dto.NombreCliente,
                ApellidoCliente = dto.ApellidoCliente,
                DireccionCliente = dto.DireccionCliente,
                CorreoCliente = dto.CorreoCliente,
                TelefonoCliente = dto.TelefonoCliente
            };

            _context.Clientes.Add(cliente);
            _context.SaveChanges(); // Guarda los cambios en la base de datos
            return cliente;
        }

        public bool EliminarCliente(int id)
        {
            var cliente = ObtenerClienteId(id);
            if (cliente == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return true;
        }

        public bool ActualizarCliente(int id, ActualizarTodoCliente clienteActualizado)
        {
            var clienteExistente = ObtenerClienteId(id);

            if (clienteExistente == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            if (string.IsNullOrWhiteSpace(clienteActualizado.NombreCliente) ||
            string.IsNullOrWhiteSpace(clienteActualizado.ApellidoCliente) ||
            string.IsNullOrWhiteSpace(clienteActualizado.DireccionCliente)) 
                    throw new ArgumentException("Datos incompletos");

            if (clienteActualizado.TelefonoCliente <= 0)
                throw new ArgumentException("Numero invalido");

            clienteExistente.NombreCliente = clienteActualizado.NombreCliente; // El cliente existente se actualiza con los nuevos valores
            clienteExistente.ApellidoCliente = clienteActualizado.ApellidoCliente;
            clienteExistente.DireccionCliente = clienteActualizado.DireccionCliente;
            clienteExistente.TelefonoCliente = clienteActualizado.TelefonoCliente;

            _context.SaveChanges();
            return true;
        }

        public bool ActualizarClientePATCH(int id, ActualizarCamposCliente dto)
        {
            var cliente = ObtenerClienteId(id);
            if (cliente == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            if (dto.NombreCliente != null)
                cliente.NombreCliente = dto.NombreCliente;

            if (dto.ApellidoCliente != null)
                cliente.ApellidoCliente = dto.ApellidoCliente;

            if (dto.DireccionCliente != null)
                cliente.DireccionCliente = dto.DireccionCliente;

            if (dto.TelefonoCliente.HasValue && dto.TelefonoCliente > 0)// Verificar que si tenga valor y sea mayor a 0
                cliente.TelefonoCliente = dto.TelefonoCliente.Value;

            _context.SaveChanges();
            return true;
        }
    }
}
