using dealership_api.Enums;
using DealershipApp.Console.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dealership_api.Models
{
    public class Ventas
    {
        [Key]
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalVenta { get; set; }
        public EstadoVenta EstadoVenta { get; set; } 
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Anticipo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoPendiente { get; set; }
        public int VehiculoId { get; set; }
        public Vehiculos Vehiculo { get; set; }
    }

}
