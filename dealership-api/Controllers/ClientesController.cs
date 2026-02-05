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
            var cliente = _clienteService.ObtenerClienteId(id); // Llamamos ala funcion 

            if (cliente == null)
                return NotFound();// Devuelve un 404

            return Ok(cliente); // si esta bien trae al empleado
        }

        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] CrearClienteDTO dto)
        {
            var clienteCreado = _clienteService.CrearCliente(dto);

            if (clienteCreado == null)
                return BadRequest(); // Devuelve un 400

            return CreatedAtAction( // Devuelve un 201
                nameof(GetById),
                new { id = clienteCreado.IdCliente },// Se esta creando la url 
                clienteCreado
                );
        }

        [HttpDelete("{id}")] // HTTP para borrar
        public ActionResult Delete(int id) // id como parametro
        {
            var eliminado = _clienteService.EliminarCliente(id); // Llamamos al servicio para eliminar
            if (!eliminado)
                return NotFound(); // Retorna 404 si no existe
            return NoContent(); // Retorna 204 si se elimina correctamente
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ActualizarTodoCliente cliente)
        {
            var actualizado = _clienteService.ActualizarCliente(id, cliente);

            if (!actualizado)
                return NotFound();

            return NoContent(); // Retorna 204 si se actualiza correctamente
        }

        [HttpPatch("{id}")]

        public ActionResult Patch(int id, [FromBody] ActualizarCamposCliente dto)
            {
            var actualizado = _clienteService.ActualizarClientePATCH(id, dto);
            if (!actualizado)
                return NotFound();
            return NoContent(); // Retorna 204 si se actualiza correctamente
        }
    }
}
                           