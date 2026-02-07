using dealership_api.Models;
using DealershipApp.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace dealership_api.Data
{
    public class DealershipDbContext : DbContext // Hereda de DbContext pero este necesita una configuración
    {
        public DealershipDbContext(DbContextOptions<DealershipDbContext> options) // DbContextOptions es la configuración que necesita DbContext
            : base(options) // Las opciones deben estar en el program.cs
        {
        }

        // Tablas
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
    }
}
