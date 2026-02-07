using dealership_api.Data;
using dealership_api.Dtos.ClientesDtos;
using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Models;

namespace dealership_api.Services
{
    public class EmpleadoService
    {
        private readonly DealershipDbContext _context;

        public EmpleadoService(DealershipDbContext context)
        {
            _context = context;
        }

        public List<Empleado> ObtenerTodosEmpleados()
        {
            return _context.Empleados.ToList(); 
        }

        public Empleado ObtenerEmpleadoId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID invalido");
            return _context.Empleados.Find(id);
        }

        public Empleado CrearEmpleado(CrearEmpleadoDTO dto)
        {
            if (dto.Salario <= 0) 
                return null;

            if (string.IsNullOrWhiteSpace(dto.Nombre) ||
                string.IsNullOrWhiteSpace(dto.Apellido) ||
                string.IsNullOrWhiteSpace(dto.Correo))
                throw new ArgumentException("Datos incompletos");

            if (!dto.Correo.Contains("@"))
                throw new ArgumentException("Correo invalido");

            if (dto.Telefono.Length != 10)
                throw new ArgumentException("El teléfono debe tener 10 dígitos");


            var empleado = new Empleado // Crear una nueva instancia de Empleado para validar con el DTO
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Cargo = dto.Cargo,
                Contraseña = dto.Contraseña,
                Salario = dto.Salario,
                FechaRegistroEmpleado = DateTime.UtcNow
            };

            _context.Empleados.Add(empleado);
            _context.SaveChanges();
            return empleado;
        }

        public bool EliminarEmpleado(int id)
        {
            var empleado = ObtenerEmpleadoId(id);
            if (empleado == null)
                throw new KeyNotFoundException("ID no encontrado");

            _context.Empleados.Remove(empleado);
            _context.SaveChanges();
            return true;
        }

        public bool ActualizarEmpleado(int id, Empleado empleadoActualizado)
        {
            var empleadoExistente = ObtenerEmpleadoId(id);
            if (empleadoExistente == null)
                throw new KeyNotFoundException("ID no encontrado");
            if (empleadoActualizado.Salario <= 0)
                throw new ArgumentException("Salario invalido");
            if (string.IsNullOrWhiteSpace(empleadoActualizado.Nombre) ||
                string.IsNullOrWhiteSpace(empleadoActualizado.Apellido) ||
                string.IsNullOrWhiteSpace(empleadoActualizado.Cargo))
                throw new ArgumentException("Datos incompletos");

            empleadoExistente.Nombre = empleadoActualizado.Nombre;
            empleadoExistente.Apellido = empleadoActualizado.Apellido;
            empleadoExistente.Salario = empleadoActualizado.Salario;
            empleadoExistente.Cargo = empleadoActualizado.Cargo;

            _context.SaveChanges();
            return true;
        }

        public bool ActualizarEmpleadoPATCH(int id, ActualizarCamposEmpleado dto)
        {
            var empleado = ObtenerEmpleadoId(id);
            if (empleado == null)
                throw new KeyNotFoundException("ID no encontrado");

            if (dto.Nombre != null)
                empleado.Nombre = dto.Nombre;

            if (dto.Apellido != null)
                empleado.Apellido = dto.Apellido;

            if (dto.Cargo != null)
                empleado.Cargo = dto.Cargo;

            if (dto.Salario.HasValue && dto.Salario > 0)// Verificar que si tenga valor y sea mayor a 0
                empleado.Salario = dto.Salario.Value;

            _context.SaveChanges();
            return true;
        }
    }
}
