using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace dealership_api.Models
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cargo { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaRegistroEmpleado { get; set; }

        [Column(TypeName="decimal(18,2)")] // Le decimos a EF que el tipo de dato en la base de datos debe ser decimal con una precisión de 18 dígitos y 2 decimales
        public decimal Salario { get; set; }
        // public bool Activo { get; set; }
    }
}
