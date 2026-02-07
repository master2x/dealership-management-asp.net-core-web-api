using dealership_api.Models;
using dealership_api.Services;
using DealershipApp.Console.Models;
using Microsoft.AspNetCore.Mvc;
using dealership_api.Dtos.ClientesDtos;

namespace dealership_api.Controllers
{

    [Route("api/[controller]")] // Ruta por defecto para los endpoints
    public class ClientesController : ControllerBase // El controlador de empleado hereda de controllerBase
    {
        private readonly ClienteService _clienteService; // Ponemos privada el servicio y se le asigna un nuevo nombre

        public ClientesController(ClienteService clienteService) // Constructor para la inyeccion de dependencias
        {
            _clienteService = clienteService;
        }

        [HttpGet]// Declaracion de metodo http (trae todos)
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Ok(_clienteService.ObtenerTodosClientes()); // Si esta bien llama ala funcion que esta en el servicio
        }

        [HttpGet("{id}")] // Va a buscar por id
        public ActionResult<Cliente> GetById(int id) // Le pasamos el objeto y parametro id
        {
            try
            {
                var cliente = _clienteService.ObtenerClienteId(id); // Llamamos ala funcion 
                return Ok(cliente); // si esta bien trae al empleado
            }
            catch (ArgumentException er)
            {
                return NotFound(er.Message); // Si no encuentra el id, devuelve un 404 con el mensaje de erro
            }
        }

        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] CrearClienteDTO dto)
        {
            try
            {
                var clienteCreado = _clienteService.CrearCliente(dto);

                return CreatedAtAction( // Devuelve un 201
                    nameof(GetById),
                    new { id = clienteCreado.IdCliente },// Se esta creando la url 
                    clienteCreado
                    );
            }
            catch (ArgumentException er)
            {
                return BadRequest(er.Message); // Devuelve un 400 con el mensaje de error

            }
        }

        [HttpDelete("{id}")] // HTTP para borrar
        public ActionResult Delete(int id) // id como parametro
        {
            try
            {
                var eliminado = _clienteService.EliminarCliente(id); // Llamamos al servicio para eliminar
                return NoContent(); // Retorna 204 si se elimina correctamente
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message); // Si no encuentra el id, devuelve un 404 con el mensaje de error
            }
           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ActualizarTodoCliente cliente)
        {
            try
            {
                _clienteService.ActualizarCliente(id, cliente);
                return NoContent();
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
        public ActionResult Patch(int id, [FromBody] ActualizarCamposCliente dto)
        {
            try
            {
                _clienteService.ActualizarClientePATCH(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }
    }
}
