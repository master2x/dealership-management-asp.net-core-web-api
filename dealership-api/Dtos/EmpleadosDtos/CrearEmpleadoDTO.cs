namespace dealership_api.Dtos.EmpleadosDtos
{

    //NO PASAMOS ID PORQUE SE GENERA AUTOMATICAMENTE EN EL SERVICIO
    public class CrearEmpleadoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
    }
}
