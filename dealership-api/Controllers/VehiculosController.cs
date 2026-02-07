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
            try
            {
                var vehiculo = _vehiculoService.ObtenerVehiculoId(id); // Llamamos ala funcion 
                return Ok(vehiculo); // si esta bien trae al empleado
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message); // Si no encuentra el id, devuelve un 404 con el mensaje de error
            }

        }

        [HttpPost]
        public ActionResult<Vehiculos> Post([FromBody] CrearVehiculoDTO dto)
        {
            try
            {
                var vehiculoCreado = _vehiculoService.CrearVehiculo(dto);
                return CreatedAtAction( // Devuelve un 201
                    nameof(GetById),
                    new { id = vehiculoCreado.IdVehiculo },// Se esta creando la url 
                    vehiculoCreado
                    );
            }
            catch (ArgumentException er)
            {
                return BadRequest(er.Message);
            }

        }

        [HttpDelete("{id}")] // HTTP para borrar
        public ActionResult Delete(int id) // id como parametro
        {
            try
            {
                _vehiculoService.EliminarVehiculo(id); // Llamamos al servicio para eliminar
                return NoContent(); // Retorna 204 si se elimina correctamente
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Vehiculos vehiculo)
        {
            try
            {
                _vehiculoService.ActualizarVehiculo(id, vehiculo);
                return NoContent(); // Retorna 204 si se actualiza correctamente
            }
            catch (ArgumentException er)
            {
                return BadRequest(er.Message);
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] ActualizarCamposVehiculo dto)
        {
            try
            {
                _vehiculoService.ActualizarVehiculoPATCH(id, dto);
                return NoContent(); // Retorna 204 si se actualiza correctamente
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }
    }
}
