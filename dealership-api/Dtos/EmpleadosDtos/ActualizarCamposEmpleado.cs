namespace dealership_api.Dtos.EmpleadosDtos
{
    public class ActualizarCamposEmpleado
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cargo { get; set; }
        public decimal? Salario { get; set; }
        public string? Telefono { get; set; }
    }
}
