using dealership_api.Enums;

namespace dealership_api.Dtos.Venta
{
    public class CrearVentaDTO
    {
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public int VehiculoId { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal Anticipo { get; set; }
    }

}
