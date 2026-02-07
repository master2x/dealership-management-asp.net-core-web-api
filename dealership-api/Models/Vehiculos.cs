using dealership_api.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dealership_api.Models
{
    public class Vehiculos
    {
        [Key]
        public int IdVehiculo { get; set; }
        public string NombreVehiculo { get; set; }
        public int Modelo { get; set; }
        public int Cantidad { get; set; }
        public bool Disponible { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public decimal Precio { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
        public DateTime FechaRegistroVehiculo { get; set; }
        public EstadoVehiculo EstadoVehiculo { get; set; }
    }
}