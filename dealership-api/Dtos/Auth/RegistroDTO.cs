using System.ComponentModel.DataAnnotations.Schema;

namespace dealership_api.Dtos.Auth
{
    public class RegistroDTO
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistroEmpleado { get; set; }

    }
}
