using dealership_api.Models;
namespace dealership_api.Services;

using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Dtos.VehiculosDtos;


public class VehiculoService
{
    private static List<Vehiculos> vehiculos = new();

    public List<Vehiculos> ObtenerTodosVehiculos()
    {
        return vehiculos;
    }

    public Vehiculos ObtenerVehiculoId(int id)                    
    {
        Console.WriteLine("Buscando vehiculo...");
        return vehiculos.FirstOrDefault(v => v.IdVehiculo == id);
    }

    public Vehiculos CrearVehiculo(CrearVehiculoDTO dto)
    {

        if (string.IsNullOrWhiteSpace(dto.NombreVehiculo) ||
            string.IsNullOrWhiteSpace(dto.Color) ||
            string.IsNullOrWhiteSpace(dto.Marca))
            return null;

       int nuevoId = vehiculos.Any()
   ? vehiculos.Max(v => v.IdVehiculo) + 1
   : 1;

        var vehiculo = new Vehiculos
        {
            IdVehiculo = nuevoId,
            NombreVehiculo = dto.NombreVehiculo,
            Modelo = dto.Modelo,
            Cantidad = dto.Cantidad,
            Disponible = dto.Disponible,
            Color = dto.Color,
            Marca = dto.Marca

        };
        vehiculos.Add(vehiculo);
        return vehiculo;
    }

    public bool EliminarVehiculo(int id)
    {
        var vehiculo = ObtenerVehiculoId(id); // Se reutiliza la funcion para poder buscar por id
        if (vehiculo == null) return false;

        vehiculos.Remove(vehiculo); // Se elimina 
        return true;
    }

    public bool ActualizarVehiculo (int id, Vehiculos vehiculoActualizado)
    {
        var vehiculoActual = ObtenerVehiculoId(id);

        if (vehiculoActual == null)
            return false;

        if (string.IsNullOrWhiteSpace(vehiculoActualizado.NombreVehiculo) ||
            string.IsNullOrWhiteSpace(vehiculoActualizado.Marca) ||
            string.IsNullOrWhiteSpace(vehiculoActualizado.Color))
            return false;

        vehiculoActual.NombreVehiculo = vehiculoActualizado.NombreVehiculo;
        vehiculoActual.Marca = vehiculoActualizado.Marca;
        vehiculoActual.Color = vehiculoActualizado.Color;

        return true;
    }

    public bool ActualizarVehiculoPATCH(int id, ActualizarCamposVehiculo dto)
    {
        var vehiculo = ObtenerVehiculoId(id);
        if (vehiculo == null)
            return false;

        if (dto.NombreVehiculo != null)
            vehiculo.NombreVehiculo = dto.NombreVehiculo;

        if (dto.Modelo.HasValue && dto.Modelo > 0)// Verificar que si tenga valor y sea mayor a 0
            vehiculo.Modelo = dto.Modelo.Value;

        if (dto.Cantidad.HasValue && dto.Cantidad >= 0)
            vehiculo.Cantidad = dto.Cantidad.Value;

        if (dto.Color != null)
            vehiculo.Color = dto.Color;

        if (dto.Marca != null)
            vehiculo.Marca = dto.Marca;

        return true;
    }


}
