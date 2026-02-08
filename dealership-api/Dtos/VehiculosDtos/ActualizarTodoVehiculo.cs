using dealership_api.Enums;

namespace dealership_api.Dtos.VehiculosDtos
{
    public class ActualizarTodoVehiculo
    {
        public string NombreVehiculo { get; set; }
        public int Modelo { get; set; }
        public int Cantidad { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public EstadoVehiculo EstadoVehiculo { get; set; }
    }
}
