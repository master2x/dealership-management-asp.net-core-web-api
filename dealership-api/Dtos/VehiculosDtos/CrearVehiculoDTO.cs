namespace dealership_api.Dtos.VehiculosDtos
{
    public class CrearVehiculoDTO
    {
        public string NombreVehiculo { get; set; }
        public int Modelo { get; set; }
        public int Cantidad { get; set; }
        public bool Disponible { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
    }
}
