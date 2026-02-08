using dealership_api.Data;
using dealership_api.Dtos.Auth;
using dealership_api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace dealership_api.Services
{
    public class AuthService
    {

        private readonly IConfiguration _configuration;

        private readonly DealershipDbContext _context; // Esto reemplaza la lista en memoria, para poder usar la base de datos

        public AuthService(DealershipDbContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        public Empleado Registrar(RegistroDTO dto)
        {
            if (_context.Empleados.Any(u => u.Correo == dto.Correo))
                throw new ArgumentException("El correo ya existe.");

            if (string.IsNullOrWhiteSpace(dto.Nombre) ||
                string.IsNullOrWhiteSpace(dto.Apellido) ||
                string.IsNullOrWhiteSpace(dto.Correo))
                throw new ArgumentException("Datos incompletos");

            if (!dto.Correo.Contains("@"))
                throw new ArgumentException("Correo invalido");

            if (dto.Telefono.Length != 10)
                throw new ArgumentException("El teléfono debe tener 10 dígitos");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Contraseña);

            var empleado = new Empleado // Crear una nueva instancia de Empleado para validar con el DTO
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Contraseña = passwordHash,
                Cargo = "Vendedor",
                FechaRegistroEmpleado = DateTime.UtcNow
            };

            _context.Empleados.Add(empleado);
            _context.SaveChanges();
            return empleado;
        }

        public string Login(LoginDTO dto)
        {
            var usuario = _context.Empleados
                .FirstOrDefault(u => u.Correo == dto.Correo);

            if (usuario == null)
                throw new ArgumentException("Credenciales inválidas.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Contraseña, usuario.Contraseña))
                throw new Exception("Credenciales inválidas.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdEmpleado.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Cargo)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            Console.WriteLine(usuario.Contraseña);
            Console.WriteLine(dto.Contraseña);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
            
        }

    }
