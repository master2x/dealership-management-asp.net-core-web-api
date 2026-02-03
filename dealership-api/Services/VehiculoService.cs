using dealership_api.Models;
namespace dealership_api.Services
{
    public class VehiculoService
    {
        private static List<Vehiculos> vehiculos = new();

        public List<Vehiculos> ObtenerTodos()
        {
            return vehiculos;
        }

        public Vehiculos ObtenerPorId(int id)                    
        {
            return vehiculos.FirstOrDefault(v => v.IdVehiculo == id);
        }

        public Vehiculos CrearVehiculo(Vehiculos vehiculo)
        {
            if (vehiculo.IdVehiculo <=0)
                return null;

            if (string.IsNullOrWhiteSpace(vehiculo.NombreVehiculo) ||
                string.IsNullOrWhiteSpace(vehiculo.Color) ||
                string.IsNullOrWhiteSpace(vehiculo.Marca))
                return null;

            if (vehiculos.Any(e => e.IdVehiculo == vehiculo.IdVehiculo))
            return null;

            vehiculos.Add(vehiculo);
            return vehiculo;
        }

        public bool EliminarVehiculo(int id)
        {
            var vehiculo = ObtenerPorId(id);
            if (vehiculo == null) return false;

            vehiculos.Remove(vehiculo);
            return true;
        }
    }
}
