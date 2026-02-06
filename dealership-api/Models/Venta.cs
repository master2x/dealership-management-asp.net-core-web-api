using System.ComponentModel.DataAnnotations;

namespace dealership_api.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdVehiculo { get; set; }
        public decimal ValorVenta { get; set; }
    }
}
