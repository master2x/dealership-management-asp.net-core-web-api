using dealership_api.Enums;
using dealership_api.Models;
using DealershipApp.Console.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dealership_api.Dtos.Venta
{
    public class CrearVentaDTO
    {
        public decimal TotalVenta { get; set; }
        public EstadoVenta EstadoVenta { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public decimal Anticipo { get; set; }
        public decimal SaldoPendiente { get; set; }
        public int VehiculoId { get; set; }
    }
}


