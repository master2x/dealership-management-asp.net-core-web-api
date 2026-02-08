using dealership_api.Dtos.Auth;
using dealership_api.Dtos.ClientesDtos;
using dealership_api.Dtos.EmpleadosDtos;
using dealership_api.Models;
using dealership_api.Services;
using DealershipApp.Console.Models;
using Microsoft.AspNetCore.Mvc;

namespace dealership_api.Controllers
{
    [ApiController] // Le indicamos que es un controlador 
    [Route("api/[controller]")] // Ruta por defecto para los endpoints
    public class EmpleadoController : ControllerBase // El controlador de empleado hereda de controllerBase
    {
        private readonly EmpleadoService _empleadoService; // Ponemos privada el servicio y se le asigna un nuevo nombre

        public EmpleadoController(EmpleadoService empleadoService) // Constructor para la inyeccion de dependencias
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]// Declaracion de metodo http (trae todos)
        public ActionResult<IEnumerable<Empleado>> Get()
        {
            return Ok(_empleadoService.ObtenerTodosEmpleados()); // Si esta bien llama ala funcion que esta en el servicio
        }

        [HttpGet("{id}")] // Va a buscar por id
        public ActionResult<Empleado> GetById(int id) // Le pasamos el objeto y parametro id
        {
            try
            {
                var empleado = _empleadoService.ObtenerEmpleadoId(id); // Llamamos ala funcion 
                return Ok(empleado); // si esta bien trae al empleado
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }

        [HttpPost]
        public ActionResult<Empleado> Post([FromBody] CrearEmpleadoDTO dto)
        {
            try
            {
                var empleadoCreado = _empleadoService.CrearEmpleado(dto);
                return CreatedAtAction( // Devuelve un 201
                    nameof(GetById),
                    new { id = empleadoCreado.IdEmpleado },// Se esta creando la url 
                    empleadoCreado
                    );
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
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
                _empleadoService.EliminarEmpleado(id); // Llamamos al servicio para eliminar
                return NoContent(); // Retorna 204 si se elimina correctamente
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Empleado empleado)
        {
            try
            {
                _empleadoService.ActualizarEmpleado(id, empleado);
                return NoContent(); // Retorna 204 si se actualiza correctamente
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
            catch (ArgumentException er)
            {
                return BadRequest(er.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] ActualizarCamposEmpleado dto)
        {
            try
            {
                _empleadoService.ActualizarEmpleadoPATCH(id, dto);
                return NoContent(); // Retorna 204 si se actualiza correctamente
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }
    }
}