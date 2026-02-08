using dealership_api.Dtos.Auth;
using dealership_api.Dtos.ClientesDtos;
using dealership_api.Models;
using dealership_api.Services;
using DealershipApp.Console.Models;
using Microsoft.AspNetCore.Mvc;

namespace dealership_api.Controllers
{

    [Route("api/[controller]")] 
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService; 

        public AuthController(AuthService authService) 
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegistroDTO dto)
        {
            var user = _authService.Registrar(dto);
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDTO dto)
        {
            var token = _authService.Login(dto);
            return Ok(new {token});
        }

    }
}
