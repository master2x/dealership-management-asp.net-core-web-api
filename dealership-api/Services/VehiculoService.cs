using dealership_api.Models;
namespace dealership_api.Services;

using dealership_api.Data;
using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Dtos.VehiculosDtos;
using Microsoft.EntityFrameworkCore;

public class VehiculoService
{
    private readonly DealershipDbContext _context;

    public VehiculoService(DealershipDbContext context)
    {
        _context = context;
    }

    public List<Vehiculos> ObtenerTodosVehiculos()
    {
        return _context.Vehiculos.ToList();
    }

    public Vehiculos ObtenerVehiculoId(int id)                    
    {
        return _context.Vehiculos.Find(id);
    }

    public Vehiculos CrearVehiculo(CrearVehiculoDTO dto)
    {

        if (string.IsNullOrWhiteSpace(dto.NombreVehiculo) ||
            string.IsNullOrWhiteSpace(dto.Color) ||
            string.IsNullOrWhiteSpace(dto.Marca) ||
            string.IsNullOrWhiteSpace(dto.Placa))
            throw new ArgumentException("Datos incompletos");

        if(dto.Precio <=0)
            throw new ArgumentException("Precio invalido");    

        if (dto.Cantidad <= 0)
            dto.EstadoVehiculo = Enums.EstadoVehiculo.NoDisponible;


        var vehiculo = new Vehiculos
        {
            NombreVehiculo = dto.NombreVehiculo,
            Modelo = dto.Modelo,
            Cantidad = dto.Cantidad,
            Color = dto.Color,
            Marca = dto.Marca,
            Placa = dto.Placa,
            Precio = dto.Precio,
            EstadoVehiculo = dto.EstadoVehiculo,
            TipoVehiculo = dto.TipoVehiculo,
            FechaRegistroVehiculo = DateTime.UtcNow

        };
        _context.Vehiculos.Add(vehiculo);
        _context.SaveChanges();
        return vehiculo;
    }

    public bool EliminarVehiculo(int id)
    {
        var vehiculo = ObtenerVehiculoId(id); // Se reutiliza la funcion para poder buscar por id
        if (vehiculo == null)
            throw new KeyNotFoundException("ID no encontrado");

        _context.Vehiculos.Remove(vehiculo); // Se elimina 
        _context.SaveChanges();
        return true;
    }

    public bool ActualizarVehiculo (int id, Vehiculos vehiculoActualizado)
    {
        var vehiculoActual = ObtenerVehiculoId(id);

        if (vehiculoActual == null)
            throw new KeyNotFoundException("ID no encontrado");

        if (string.IsNullOrWhiteSpace(vehiculoActualizado.NombreVehiculo) ||
            string.IsNullOrWhiteSpace(vehiculoActualizado.Marca) ||
            string.IsNullOrWhiteSpace(vehiculoActualizado.Color))
            throw new ArgumentException("Datos incompletos");

        vehiculoActual.NombreVehiculo = vehiculoActualizado.NombreVehiculo;
        vehiculoActual.Marca = vehiculoActualizado.Marca;
        vehiculoActual.Color = vehiculoActualizado.Color;

        return true;
    }

    public bool ActualizarVehiculoPATCH(int id, ActualizarCamposVehiculo dto)
    {
        var vehiculo = ObtenerVehiculoId(id);
        if (vehiculo == null)
            throw new KeyNotFoundException("ID no encontrado");

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
