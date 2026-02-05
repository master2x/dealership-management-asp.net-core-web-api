using dealership_api.Dtos.ClientesDtos;
using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Models;

namespace dealership_api.Services
{
    public class EmpleadoService
    {
        private static List<Empleado> empleados = new();

        public List<Empleado> ObtenerTodosEmpleados()
        {
            return empleados; 
        }

        public Empleado ObtenerEmpleadoId(int id)
        {
            return empleados.FirstOrDefault(e => e.IdEmpleado == id);
        }

        public Empleado CrearEmpleado(CrearEmpleadoDTO dto)
        {
            if (dto.Salario <= 0) 
                return null;

            if (string.IsNullOrWhiteSpace(dto.Nombre) ||
                string.IsNullOrWhiteSpace(dto.Apellido) ||
                string.IsNullOrWhiteSpace(dto.Correo))
                return null;

            if (!dto.Correo.Contains("@"))
                return null;

            int nuevoId = empleados.Any() // SIEMPRE HAY QUE CREAR UN NUEVO ID, EL USUARIO NO LO TIENE QUE ENVIAR
      ? empleados.Max(e => e.IdEmpleado) + 1
      : 1;

            var empleado = new Empleado // Crear una nueva instancia de Empleado para validar con el DTO
            {
                IdEmpleado = nuevoId,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Cargo = dto.Cargo,
                Salario = dto.Salario
            };

            empleados.Add(empleado);
            return empleado;
        }

        public bool EliminarEmpleado(int id)
        {
            var empleado = ObtenerEmpleadoId(id);
            if (empleado == null) return false;

            empleados.Remove(empleado);
            return true;
        }

        public bool ActualizarEmpleado(int id, Empleado empleadoActualizado)
        {
            var empleadoExistente = ObtenerEmpleadoId(id);
            if (empleadoExistente == null)
                return false;
            if (empleadoActualizado.Salario <= 0)
                return false;
            if (string.IsNullOrWhiteSpace(empleadoActualizado.Nombre) ||
                string.IsNullOrWhiteSpace(empleadoActualizado.Apellido) ||
                string.IsNullOrWhiteSpace(empleadoActualizado.Cargo))
                return false;
            
            empleadoExistente.Nombre = empleadoActualizado.Nombre;
            empleadoExistente.Apellido = empleadoActualizado.Apellido;
            empleadoExistente.Salario = empleadoActualizado.Salario;
            empleadoExistente.Cargo = empleadoActualizado.Cargo;
            return true;
        }

        public bool ActualizarEmpleadoPATCH(int id, ActualizarCamposEmpleado dto)
        {
            var empleado = ObtenerEmpleadoId(id);
            if (empleado == null)
                return false;

            if (dto.Nombre != null)
                empleado.Nombre = dto.Nombre;

            if (dto.Apellido != null)
                empleado.Apellido = dto.Apellido;

            if (dto.Cargo != null)
                empleado.Cargo = dto.Cargo;

            if (dto.Salario.HasValue && dto.Salario > 0)// Verificar que si tenga valor y sea mayor a 0
                empleado.Salario = dto.Salario.Value;

            return true;
        }
    }
}
