using dealership_api.Enums;

namespace dealership_api.Dtos.VehiculosDtos
{
    public class CrearVehiculoDTO
    {
        public string NombreVehiculo { get; set; }
        public int Modelo { get; set; }
        public int Cantidad { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public decimal Precio { get; set; }
        public EstadoVehiculo EstadoVehiculo { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
    }
}
