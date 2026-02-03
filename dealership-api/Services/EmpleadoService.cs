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

        public Empleado CrearEmpleado(Empleado empleado)
        {
            if (empleado.Salario <= 0) // crear automaticamente el id
                return null;

            if (string.IsNullOrWhiteSpace(empleado.Nombre) ||
                string.IsNullOrWhiteSpace(empleado.Apellido) ||
                string.IsNullOrWhiteSpace(empleado.Correo))
                return null;

            if (!empleado.Correo.Contains("@"))
                return null;

            if (empleado.IdEmpleado <= 0)
                return null;

            if (empleados.Any(e => e.IdEmpleado == empleado.IdEmpleado))
                return null;

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
    }
}
