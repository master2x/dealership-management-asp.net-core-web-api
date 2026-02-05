using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Dtos.VehiculosDtos;
using dealership_api.Models;
using dealership_api.Services;
using DealershipApp.Console.Models;
using Microsoft.AspNetCore.Mvc;

namespace dealership_api.Controllers
{

    [Route("api/[controller]")] // Ruta por defecto para los endpoints
    public class VehiculosController : ControllerBase // El controlador de empleado hereda de controllerBase
    {
        private readonly VehiculoService _vehiculoService; // Ponemos privada el servicio y se le asigna un nuevo nombre

        public VehiculosController(VehiculoService vehiculoService) // Constructor para la inyeccion de dependencias
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]// Declaracion de metodo http (trae todos)
        public ActionResult<IEnumerable<Vehiculos>> Get()
        {
            return Ok(_vehiculoService.ObtenerTodosVehiculos()); // Si esta bien llama ala funcion que esta en el servicio
        }

        [HttpGet("{id}")] // Va a buscar por id
        public ActionResult<Vehiculos> GetById(int id) // Le pasamos el objeto y parametro id
        {
            var vehiculo = _vehiculoService.ObtenerVehiculoId(id); // Llamamos ala funcion 

            if (vehiculo == null)
                return NotFound();// Devuelve un 404

            return Ok(vehiculo); // si esta bien trae al empleado
        }

        [HttpPost]
        public ActionResult<Vehiculos> Post([FromBody] CrearVehiculoDTO dto)
        {
            var vehiculoCreado = _vehiculoService.CrearVehiculo(dto);

            if (vehiculoCreado == null)
                return BadRequest(); // Devuelve un 400

            return CreatedAtAction( // Devuelve un 201
                nameof(GetById),
                new { id = vehiculoCreado.IdVehiculo },// Se esta creando la url 
                vehiculoCreado
                );
        }

        [HttpDelete("{id}")] // HTTP para borrar
        public ActionResult Delete(int id) // id como parametro
        {
            var eliminado = _vehiculoService.EliminarVehiculo(id); // Llamamos al servicio para eliminar
            if (!eliminado)
                return NotFound(); // Retorna 404 si no existe
            return NoContent(); // Retorna 204 si se elimina correctamente
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Vehiculos vehiculo)
        {
            var actualizado = _vehiculoService.ActualizarVehiculo(id, vehiculo);

            if (!actualizado)
                return NotFound();

            return NoContent(); // Retorna 204 si se actualiza correctamente
        }

        [HttpPatch("{id}")]

        public ActionResult Patch(int id, [FromBody] ActualizarCamposVehiculo dto)
        {
            var actualizado = _vehiculoService.ActualizarVehiculoPATCH(id, dto);
            if (!actualizado)
                return NotFound();
            return NoContent(); // Retorna 204 si se actualiza correctamente
        }
    }
}
