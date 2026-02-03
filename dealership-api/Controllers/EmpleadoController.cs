using dealership_api.Models;
using dealership_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dealership_api.Controllers
{

    [ApiController] // Le indicamos que es un controlador 
    [Route("api/[controller]")] // Ruta por defecto para los endpoints
    public class EmpleadoController : ControllerBase // El controlador de empleado hereda de controllerBase
    {
        private readonly EmpleadoService _empleadoService; // Ponemos privada el servicio y se le asigna un nuevo nombre
        
        public EmpleadoController (EmpleadoService empleadoService) // Constructor para la inyeccion de dependencias
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
            var empleado = _empleadoService.ObtenerEmpleadoId(id); // Llamamos ala funcion 

            if (empleado == null)
                return NotFound();// Devuelve un 404

            return Ok(empleado); // si esta bien trae al empleado
        }

        [HttpPost]
        public ActionResult<Empleado> Post([FromBody] Empleado empleado)
        {
            var empleadoCreado = _empleadoService.CrearEmpleado(empleado);

            return CreatedAtAction( // Devuelve un 201
                nameof(GetById),
                new { id = empleadoCreado.IdEmpleado },// Se esta creando la url 
                empleadoCreado
                );
        }
    }
}
