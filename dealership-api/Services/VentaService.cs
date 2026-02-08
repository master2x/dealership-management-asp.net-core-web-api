using dealership_api.Data;
using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Dtos.VehiculosDtos;
using dealership_api.Dtos.Venta;
using dealership_api.Enums;
using dealership_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dealership_api.Services;

public class VentaService
{
    private readonly DealershipDbContext _context;
    
    public VentaService(DealershipDbContext context)
    {
        _context = context;
    }

    public List<Ventas> ObtenerTodasVentas()
    {
        return _context.Ventas.ToList();
    }

    public Ventas ObtenerVentaId(int id)
    {
        return _context.Ventas.Find(id);

    }

    public Ventas CrearVenta(CrearVentaDTO dto)
    {
        if (dto.TotalVenta <= 0)
            throw new ArgumentException("El total de la venta debe ser mayor a cero.");

        if (dto.Anticipo < 0 || dto.Anticipo > dto.TotalVenta)
            throw new ArgumentException("El anticipo no es válido.");

        var vehiculo = _context.Vehiculos.Find(dto.VehiculoId);
        if (vehiculo == null)
            throw new KeyNotFoundException("El vehículo no existe.");

        var cliente = _context.Clientes.Find(dto.ClienteId)
            ?? throw new KeyNotFoundException("El cliente no existe.");

        var empleado = _context.Empleados.Find(dto.EmpleadoId)
            ?? throw new KeyNotFoundException("El empleado no existe.");

        var saldo = dto.TotalVenta - dto.Anticipo;

        var venta = new Ventas
        {
            FechaVenta = DateTime.Now,
            TotalVenta = dto.TotalVenta,
            Anticipo = dto.Anticipo,
            SaldoPendiente = saldo,
            EstadoVenta = saldo == 0 ? EstadoVenta.Pagada : EstadoVenta.AnticipoPagado,
            ClienteId = dto.ClienteId,
            EmpleadoId = dto.EmpleadoId,
            VehiculoId = dto.VehiculoId
        };

        vehiculo.Cantidad -= 1;
        vehiculo.EstadoVehiculo = vehiculo.Cantidad > 0
            ? EstadoVehiculo.Reservado
            : EstadoVehiculo.NoDisponible;

        _context.Ventas.Add(venta);
        _context.SaveChanges();

        return venta;
    }

    public Ventas AnularVenta(int id)
    {
        var venta = ObtenerVentaId(id);
        if (venta == null)
            throw new KeyNotFoundException("La venta no existe.");

        if (venta.EstadoVenta == EstadoVenta.Cancelada)
            throw new InvalidOperationException("La venta ya está cancelada.");

        if (venta.EstadoVenta == EstadoVenta.Pagada)
            throw new InvalidOperationException("No se puede cancelar una venta pagada.");

        venta.EstadoVenta = EstadoVenta.Cancelada;

        var vehiculo = _context.Vehiculos.Find(venta.VehiculoId);
        vehiculo.Cantidad += 1;
        vehiculo.EstadoVehiculo = EstadoVehiculo.Disponible;

        _context.SaveChanges();
        return venta;
    }
}