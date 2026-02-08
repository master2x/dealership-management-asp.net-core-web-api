using dealership_api.Dtos.Venta;
using dealership_api.Models;
using dealership_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dealership_api.Controllers
{

    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly VentaService _ventaService; // Ponemos privada el servicio y se le asigna un nuevo nombre

        public VentaController(VentaService ventaService) // Constructor para la inyeccion de dependencias
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ventas>> Get()
        {
            return Ok(_ventaService.ObtenerTodasVentas());
        }

        [HttpGet("{id}")]
        public ActionResult<Ventas>GetById(int id)
        {
            try
            {
                var venta = _ventaService.ObtenerVentaId(id);
                return Ok(venta);

            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
        }

        [HttpPost]
        public ActionResult<Ventas> Post([FromBody]  CrearVentaDTO dto)
        {
            try
            {
                var ventaCreada = _ventaService.CrearVenta(dto);
                return CreatedAtAction( // Devuelve un 201
                    nameof(GetById),
                    new { id = ventaCreada.IdVenta },// Se esta creando la url 
                    ventaCreada
                    );
            }
            catch (ArgumentException er)
            {
                return BadRequest(er.Message);
            }
        }

        [HttpPut("{id}/anular")]
        public IActionResult AnularVenta(int id)
        {
            try
            {
                var venta = _ventaService.AnularVenta(id);
                return Ok(venta);
            }
            catch (KeyNotFoundException er)
            {
                return NotFound(er.Message);
            }
            catch (InvalidOperationException er)
            {
                return BadRequest(er.Message);
            }
            
        }

    }
}
